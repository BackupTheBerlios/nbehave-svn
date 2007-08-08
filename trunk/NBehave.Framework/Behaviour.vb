Option Strict On

Imports NBehave.Framework.Story
Imports System.Collections.ObjectModel


'TODO
' A console runner
' A runner

Public Delegate Sub Action(Of T)(ByVal value As T)


'Public Class BehaviourAttribute
'    Inherits Attribute
'End Class


'optional values dont "work" in C#
Public Interface IScenarioGiven
    Function [Given](Of T)(ByVal description As String, ByVal value As T) As IScenarioGivenAnd
    Function [Given](Of T)(ByVal description As String, ByVal value As T, ByVal theGiven As Action(Of T)) As IScenarioGivenAnd
    'Function Given(ByVal description As String, ByVal ParamArray stuff() As Object) As IScenarioGivenAnd
End Interface

Public Interface IScenarioGivenAnd
    Function [And](Of T)(ByVal description As String, ByVal value As T) As IScenarioGivenAnd
    Function [And](Of T)(ByVal description As String, ByVal value As T, ByVal given As Action(Of T)) As IScenarioGivenAnd
    Function [When](Of T)(ByVal description As String, ByVal value As T) As IScenarioWhen
    Function [When](Of T)(ByVal description As String, ByVal value As T, ByVal [event] As Action(Of T)) As IScenarioWhen
End Interface

Public Interface IScenarioWhen
    Function [Then](Of T)(ByVal description As String, ByVal value As T) As IScenarioThen
    Function [Then](Of T)(ByVal description As String, ByVal value As T, ByVal validator As Action(Of T)) As IScenarioThen
End Interface

Public Interface IScenarioThen
    Function [And](Of T)(ByVal description As String, ByVal value As T) As IScenarioThen
    Function [And](Of T)(ByVal description As String, ByVal value As T, ByVal validator As Action(Of T)) As IScenarioThen
End Interface



Public Class Behaviour 'should probably be named Story
    Implements INarrativeAsA
    Implements IScenarioGiven
    Implements IScenarioGivenAnd
    Implements IScenarioWhen
    Implements IScenarioThen
    Implements IStoryBase


    Public MustInherit Class GivenWhenThenBase
        Public Description As String
        Public Value As Object
        Private Action As [Delegate]

        Protected Sub New(ByVal description As String, ByVal value As Object, ByVal action As [Delegate])
            Me.Description = description
            Me.Value = value
            Me.Action = action
        End Sub

        Public Sub Invoke()
            Dim p() As Object = {Value}
            Action.DynamicInvoke(p)
        End Sub
    End Class

    Public Class Given_
        Inherits GivenWhenThenBase
        Sub New(ByVal description As String, ByVal value As Object, ByVal theGiven As [Delegate])
            MyBase.New(description, value, theGiven)
        End Sub
    End Class
    Public Class Event_
        Inherits GivenWhenThenBase
        Sub New(ByVal description As String, ByVal value As Object, ByVal [event] As [Delegate])
            MyBase.New(description, value, [event])
        End Sub
    End Class
    Public Class Outcome_
        Inherits GivenWhenThenBase
        Sub New(ByVal description As String, ByVal value As Object, ByVal validator As [Delegate])
            MyBase.New(description, value, validator)
        End Sub
    End Class

    Public Class Scenario_
        Public Description As String = "Not specified"
        Public Givens As IList(Of Given_) = New List(Of Given_)
        Public [Event] As Event_
        Public Outcomes As IList(Of Outcome_) = New List(Of Outcome_)

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


    End Class



    Public Overridable Sub Setup()
    End Sub


#Region "Story"

    Private _narrative As Narrative = New Narrative()
    Private _storyTitle As String = String.Empty

    Public Event ScenarioOutcome(ByVal sender As Object, ByVal e As NBehaveEventArgs) Implements IStoryBase.ScenarioOutcome


    Public ReadOnly Property Narrative() As Narrative Implements IStoryBase.Narrative
        Get
            Return _narrative
        End Get
    End Property



    Private Sub Story1() Implements Story.IStoryBase.Story
        Throw New NotImplementedException
    End Sub

    'Runs the 
    Private Function Run() As Outcome Implements IStoryBase.Run
        Throw New NotImplementedException
    End Function

    Dim outcome As New Outcome(False, "Not verified")
    Private ensurer As New Ensure(Of Behaviour)(outcome)

    Protected Function Ensure() As Ensure(Of Behaviour)
        Return ensurer
    End Function


    Public ReadOnly Property StoryTitle() As String
        Get
            Return _storyTitle
        End Get
    End Property

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
        Return Me
    End Function

#End Region


#Region "Scenario"

    Private _scenarios As IList(Of Scenario_) = New List(Of Scenario_)

    Public ReadOnly Property Scenarios() As ReadOnlyCollection(Of Scenario_)
        Get
            Return New ReadOnlyCollection(Of Scenario_)(_scenarios)
        End Get
    End Property

    'The scenario being setup by Given/When/Then
    Private _scenario As Scenario_


    Protected Function Scenario(ByVal name As String) As IScenarioGiven
        _scenario = New Scenario_
        _scenario.Description = name
        _scenarios.Add(_scenario)
        Return Me
    End Function



#End Region


#Region "Interface implementations"

    'These 4 methods should be in their own class

    Private Function AsA(ByVal role As String) As INarrativeIWant Implements Story.INarrativeAsA.AsA
        Return _narrative.AsA(role)
    End Function

    Private Function Given(Of T)(ByVal description As String, ByVal value As T) As IScenarioGivenAnd Implements IScenarioGiven.Given, IScenarioGivenAnd.And
        Return Given(description, value, Nothing)
    End Function

    Private Function Given(Of T)(ByVal description As String, ByVal value As T, ByVal theGiven As Action(Of T)) As IScenarioGivenAnd Implements IScenarioGiven.Given, IScenarioGivenAnd.And
        _scenario.Givens.Add(New Given_(description, value, theGiven))
        Return Me
    End Function

    Private Function [When](Of T)(ByVal description As String, ByVal value As T) As IScenarioWhen Implements IScenarioGivenAnd.When
        Return [When](description, value, Nothing)
    End Function

    Private Function [When](Of T)(ByVal description As String, ByVal value As T, ByVal [event] As Action(Of T)) As IScenarioWhen Implements IScenarioGivenAnd.When
        _scenario.Event = New Event_(description, value, [event])
        Return Me
    End Function

    Private Function [Then](Of T)(ByVal description As String, ByVal expected As T) As IScenarioThen Implements IScenarioWhen.Then, IScenarioThen.And
        Return [Then](description, expected, Nothing)
    End Function

    Private Function [Then](Of T)(ByVal description As String, ByVal expected As T, ByVal validator As Action(Of T)) As IScenarioThen Implements IScenarioWhen.Then, IScenarioThen.And
        _scenario.Outcomes.Add(New Outcome_(description, expected, validator))
        Return Me
    End Function

#End Region


End Class