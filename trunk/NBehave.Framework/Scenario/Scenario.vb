Option Strict On

Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports NBehave.Framework.Utility


Namespace Scenario


    'TODO: FluentInterface in its own class?
    Public MustInherit Class Scenario
        Implements FluentInterface.IGivenStart
        Implements FluentInterface.IGiven
        Implements FluentInterface.IEvent
        Implements FluentInterface.IOutcome
        Implements IScenario


        Public MustOverride Sub Specify() Implements IScenario.Specify
        Public MustOverride Function SetupWorld() As Object Implements IScenario.SetupWorld


        Private _givens As IList(Of World.IGiven)
        Private _event As World.[IEvent]
        Private _outcomes As IList(Of World.IWorldOutcome)
        Private _world As Object


        Protected Sub New()
            Me.New(New List(Of World.IGiven), Nothing, New List(Of World.IWorldOutcome), Nothing)
        End Sub


        Protected Sub New(ByVal givens As IList(Of World.IGiven), ByVal [event] As World.[IEvent], ByVal outcomes As IList(Of World.IWorldOutcome), ByVal world As Object)
            _givens = givens
            _event = [event]
            _outcomes = outcomes
            _world = world
        End Sub


        Protected Property World() As Object Implements IScenario.World
            Get
                Return _world
            End Get
            Set(ByVal Value As Object)
                _world = Value
            End Set
        End Property


        Public ReadOnly Property [Event]() As World.IEvent
            Get
                Return _event
            End Get
        End Property


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Running the scenario
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public Function Run() As Outcome Implements IScenario.Run
            '_outcomes.Clear()

            Specify()
            World = SetupWorld()
            SetupGivens()
            WorldEvent()

            Return VerifyOutcomes()

        End Function


        Public Sub SetupGivens()
            For Each g As World.IGiven In _givens
                g.Setup(Me.World)
            Next
        End Sub


        Public Sub WorldEvent()
            _event.OccurIn(Me.World)
        End Sub


        Public Function VerifyOutcomes() As Outcome
            Dim outcomeResult As New Outcome(Framework.OutcomeResult.Passed, "")
            For Each o As World.IWorldOutcome In _outcomes
                Try
                    o.Verify(Me.World)
                Catch ex As Exception
                    o.Result = New Outcome(Framework.OutcomeResult.Failed, ex.ToString)
                End Try
                outcomeResult.AddOutcome(o.Result)
            Next

            Return outcomeResult

        End Function


        Protected Overridable ReadOnly Property Title() As String Implements IScenarioBase.Title
            Get
                Return CamelCaseToNormalSentence(Me.GetType.Name)
            End Get
        End Property




#Region "FluentInterface implementation"


        Public Function Given(ByVal description As String, ByVal theGiven As World.IGiven) As FluentInterface.IGiven Implements FluentInterface.IGivenStart.Given, IScenario.Given, FluentInterface.IGiven.And
            _givens.Add(theGiven)
            Return Me
        End Function



        Private Function [When](ByVal description As String, ByVal theEvent As World.[IEvent]) As FluentInterface.IEvent Implements FluentInterface.IGiven.When
            _event = theEvent
            Return Me
        End Function


        Private Function [Then](ByVal description As String, ByVal outcome As World.IWorldOutcome) As FluentInterface.IOutcome Implements FluentInterface.IEvent.Then, FluentInterface.IOutcome.And
            _outcomes.Add(outcome)
            Return Me
        End Function

#End Region

    End Class


End Namespace