Option Strict On

Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Story
Imports System.Collections.ObjectModel

'TODO: Lot of refactoring, this is ugly...



Public Class Behaviour 'should probably be named Story
    Implements IStoryBase


    Public MustInherit Class GivenWhenThenBase
        Public Description As String
        Public Values() As Object
        Private Action As [Delegate]

        Protected Sub New(ByVal description As String, ByVal values() As Object, ByVal action As [Delegate])
            Me.Description = description
            Me.Values = values
            Me.Action = action
        End Sub

        Public ReadOnly Property CanInvoke() As Boolean
            Get
                Return Action IsNot Nothing
            End Get
        End Property

        Public Sub Invoke()
            Action.DynamicInvoke(Values)
        End Sub
    End Class

    Public Class Given_
        Inherits GivenWhenThenBase
        Sub New(ByVal description As String, ByVal values() As Object, ByVal theGiven As [Delegate])
            MyBase.New(description, values, theGiven)
        End Sub
    End Class
    Public Class Event_
        Inherits GivenWhenThenBase
        Sub New(ByVal description As String, ByVal values() As Object, ByVal [event] As [Delegate])
            MyBase.New(description, values, [event])
        End Sub
    End Class
    Public Class Outcome_
        Inherits GivenWhenThenBase
        Sub New(ByVal description As String, ByVal values() As Object, ByVal validator As [Delegate])
            MyBase.New(description, values, validator)
        End Sub
    End Class

    Public Class Scenario_
        Implements IScenarioBase

        Private _title As String = "Not specified"
        Public Givens As IList(Of Given_) = New List(Of Given_)
        Public [Event] As Event_
        Public Outcomes As IList(Of Outcome_) = New List(Of Outcome_)


        Public Sub New(ByVal title As String)
            _title = title
        End Sub

        Public Function Run() As Outcome
            Given() : [When]() : [Then]()
            Return Nothing
        End Function

        Private Sub Given()
            For Each given As Given_ In Givens
                given.Invoke()
            Next
        End Sub

        Private Sub [When]()
            [Event].Invoke()
        End Sub
        Private Sub [Then]()
            For Each outcome As Outcome_ In Outcomes
                outcome.Invoke()
            Next
        End Sub


        Public ReadOnly Property Description() As String Implements Scenario.IScenarioBase.Title
            Get
                Return _title
            End Get
        End Property
    End Class



    Protected Overridable Sub Setup()
    End Sub


#Region "Story"

    Private _narrative As Narrative = New Narrative()
    Private _storyTitle As String = String.Empty

    Private outcome As New Outcome(OutcomeResult.Failed, "Not verified")

    Private ensurer As New Ensure(Of Behaviour)(outcome)    'So we can "catch" the result


    Public Event ScenarioOutcome(ByVal sender As Object, ByVal e As NBehaveEventArgs) Implements IStoryBase.ScenarioOutcome


    Public ReadOnly Property Narrative() As Narrative Implements IStoryBase.Narrative
        Get
            Return _narrative
        End Get
    End Property


    Protected ReadOnly Property StoryTitle() As String Implements IStoryBase.Title
        Get
            Return _storyTitle
        End Get
    End Property


    Protected Overridable Sub Story1() Implements Story.IStoryBase.Story
        Throw New NotImplementedException
    End Sub


    'Runs it all
    Protected Function Run() As Outcome Implements IStoryBase.Run
        Dim scenarioOutcomes As New List(Of Outcome)

        For Each scenario As Scenario_ In _scenarios
            Dim scenarioOutcome As Outcome

            'scenarioOutcomes = New List(Of Outcome)
            scenarioOutcome = ExecuteScenario(scenario)
            If scenarioOutcome.Result = OutcomeResult.Passed Then
                If Not AllItemsHaveAction(scenario) Then
                    scenarioOutcome.Result = OutcomeResult.Pending
                    scenarioOutcome.Message = "Something is missing an action"
                End If
            End If
            scenarioOutcomes.Add(scenarioOutcome)
        Next
        Dim storyOutcome As Outcome = CreateOutcome(scenarioOutcomes)

        Return storyOutcome

    End Function

    Private Function AllItemsHaveAction(ByVal scenario As Scenario_) As Boolean
        Dim missingAction As Boolean = False

        For Each g As Given_ In scenario.Givens
            If Not g.CanInvoke Then
                missingAction = True
                Exit For
            End If
        Next
        If Not scenario.Event.CanInvoke Then missingAction = True

        If Not missingAction Then
            For Each o As Outcome_ In scenario.Outcomes
                If o.CanInvoke Then
                    missingAction = True
                    Exit For
                End If
            Next
        End If

        Return Not missingAction

    End Function


    Private Function ExecuteScenario(ByVal scenario As Scenario_) As Outcome
        Dim thenOutcomes As List(Of Outcome)

        InvokeGivens(scenario)
        InvokeEvent(scenario)
        thenOutcomes = InvokeOutcomes(scenario)

        Dim scenarioOutcome As Outcome = CreateOutcome(thenOutcomes)
        RaiseEvent ScenarioOutcome(scenario, New NBehaveEventArgs(scenarioOutcome))

        Return scenarioOutcome

    End Function


    Private Function InvokeOutcomes(ByRef scenario As Scenario_) As List(Of Outcome)
        Dim thenOutcomes As New List(Of Outcome)

        For Each o As Outcome_ In scenario.Outcomes
            If o.CanInvoke Then
                o.Invoke()
                thenOutcomes.Add(outcome)
            Else
                thenOutcomes.Add(New Outcome(OutcomeResult.Pending, "The outcome has no action"))
            End If
        Next

        Return thenOutcomes

    End Function


    Private Sub InvokeEvent(ByRef scenario As Scenario_)
        If scenario.Event.CanInvoke Then scenario.Event.Invoke()
    End Sub


    Private Sub InvokeGivens(ByRef scenario As Scenario_)
        For Each g As Given_ In scenario.Givens
            If g.CanInvoke Then g.Invoke()
        Next
    End Sub


    Private Function CreateOutcome(ByVal outcomes As IList(Of Outcome)) As Outcome
        Dim arr(outcomes.Count - 1) As Outcome
        outcomes.CopyTo(arr, 0)
        Dim finalOutcome As New Outcome(arr)

        Return finalOutcome

    End Function

    Protected Overridable Function Ensure() As Ensure(Of Behaviour)
        Return ensurer
    End Function

    Protected Function Story() As Story.INarrativeAsA
        'The try/finally block prevents inling. Need to do that so I always get the same behaviour (Debugging never inlines methods).
        Try
            Dim stack As New System.Diagnostics.StackFrame(1)
            Return Story(Utility.CamelCaseToNormalSentence(stack.GetMethod().Name()))
        Finally
        End Try
    End Function

    Protected Function Story(ByVal title As String) As Story.INarrativeAsA
        'new story, clear out old scenarios
        _scenarios.Clear()
        Me._storyTitle = title

        Dim _fluentStory As New FluentStory(_narrative)   'As a x, I want y, So that z

        Return _fluentStory

    End Function

#End Region


#Region "Scenario"

    Private _scenarios As IList(Of Scenario_) = New List(Of Scenario_)


    Public ReadOnly Property Scenarios() As ReadOnlyCollection(Of Scenario_)
        Get
            Return New ReadOnlyCollection(Of Scenario_)(_scenarios)
        End Get
    End Property


    ''' <summary>
    ''' This is the starting point for describing a scenario with its Given, When and Then's
    ''' </summary>
    ''' <param name="title"></param>
    ''' <returns>an instance of IScenarioGiven</returns>
    ''' <remarks></remarks>
    Protected Function Scenario(ByVal title As String) As IScenarioGiven
        Dim _scenario As New Scenario_(title)
        _scenarios.Add(_scenario)

        Return New FluentScenario(_scenario)

    End Function



#End Region




End Class