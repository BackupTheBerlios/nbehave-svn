Option Strict On

Imports System.Collections.ObjectModel


Namespace Scenario


    Public Interface IScenario(Of T)
        Function Given(ByVal theGiven As World.IGiven(Of T)) As FluentInterface.IGiven(Of T)
        Property World() As T
        Sub Specify()
        Function SetupWorld() As T
        Function Run() As ReadOnlyCollection(Of ScenarioOutcome)
    End Interface


End Namespace
