Option Strict On

Imports System.Collections.ObjectModel


Namespace Scenario

    Public Interface IScenarioBase
        ReadOnly Property Title() As String
    End Interface

    Public Interface IScenario
        Inherits IScenarioBase
        'Delegate Function AGiven(Of G)(ByVal params() As Object) As G

        Property World() As Object
        Sub Specify()
        Function SetupWorld() As Object
        Function Run() As Outcome

        'This is the starting point when specifying your given/when & then's
        Function Given(ByVal description As String, ByVal theGiven As World.IGiven) As FluentInterface.IGiven

    End Interface


End Namespace
