Option Strict On

Imports System.Collections.ObjectModel
Imports System.Collections.Generic

Namespace Scenario

    Public Class WorldOutcomeCollection(Of T)
        Inherits Collection(Of World.IWorldOutcome(Of T))
    End Class

    Public Class GivenCollection(Of T)
        Inherits Collection(Of World.IGiven(Of T))
    End Class


    Public MustInherit Class Scenario(Of T)
        Implements FluentInterface.IGiven(Of T)
        Implements FluentInterface.IEvent(Of T)
        Implements FluentInterface.IOutcome(Of T)
        Implements IScenario(Of T)


        Public MustOverride Sub Specify() Implements IScenario(Of T).Specify
        Public MustOverride Function SetupWorld() As T Implements IScenario(Of T).SetupWorld


        Private _givens As GivenCollection(Of T)
        Private _event As World.[IEvent](Of T)
        Private _outcomes As WorldOutcomeCollection(Of T)
        Private _world As T


        Protected Sub New()
            Me.New(New GivenCollection(Of T), Nothing, New WorldOutcomeCollection(Of T), Nothing)
        End Sub


        Protected Sub New(ByVal givens As GivenCollection(Of T), ByVal [event] As World.[IEvent](Of T), ByVal outcomes As WorldOutcomeCollection(Of T), ByVal world As T)
            _givens = givens
            _event = [event]
            _outcomes = outcomes
            _world = world
        End Sub


        Protected Property World() As T Implements IScenario(Of T).World
            Get
                Return _world
            End Get
            Set(ByVal Value As T)
                _world = Value
            End Set
        End Property


        Public ReadOnly Property [Event]() As World.[IEvent](Of T)
            Get
                Return _event
            End Get
        End Property


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Running the scenario
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Public Function Run() As ReadOnlyCollection(Of ScenarioOutcome) Implements IScenario(Of T).Run
            Specify()
            World = SetupWorld()
            SetupGivens()
            WorldEvent()
            Dim o As IList(Of ScenarioOutcome) = VerifyOutcomes()

            Return New ReadOnlyCollection(Of ScenarioOutcome)(o)

        End Function


        Public Sub SetupGivens()
            For Each g As World.IGiven(Of T) In _givens
                g.Setup(Me.World)
            Next
        End Sub


        Public Sub WorldEvent()
            _event.OccurIn(Me.World)
        End Sub


        Public Function VerifyOutcomes() As ReadOnlyCollection(Of ScenarioOutcome)
            Dim outcomeResults As IList(Of ScenarioOutcome) = New List(Of ScenarioOutcome)
            For Each o As World.IWorldOutcome(Of T) In _outcomes
                o.Verify(Me.World)
                outcomeResults.Add(o.Result)
            Next

            Return New ReadOnlyCollection(Of ScenarioOutcome)(outcomeResults)

        End Function



#Region "FluentInterface implementation"


        Public Function Given(ByVal theGiven As World.IGiven(Of T)) As FluentInterface.IGiven(Of T) Implements IScenario(Of T).Given, FluentInterface.IGiven(Of T).And
            _givens.Add(theGiven)
            Return Me
        End Function


        Private Function [When](ByVal theEvent As World.[IEvent](Of T)) As FluentInterface.IEvent(Of T) Implements FluentInterface.IGiven(Of T).When
            _event = theEvent
            Return Me
        End Function


        Private Function [Then](ByVal outcome As World.IWorldOutcome(Of T)) As FluentInterface.IOutcome(Of T) Implements FluentInterface.IEvent(Of T).Then, FluentInterface.IOutcome(Of T).And
            _outcomes.Add(outcome)
            Return Me
        End Function

#End Region


    End Class


End Namespace