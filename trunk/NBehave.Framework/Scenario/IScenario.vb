Option Strict On

Imports System.Collections.ObjectModel


Namespace Scenario


    Public Interface IScenario(Of T)
        Delegate Function AGiven(Of G)(ByVal params() As Object) As G

        Function Given(ByVal theGiven As World.IGiven(Of T)) As FluentInterface.IGiven(Of T)
        Function Given(ByVal nameOfGiven As String, ByVal valueOfGiven As T, ByVal theGiven As AGiven(Of T)) As FluentInterface.IGiven(Of T)

        Property World() As T
        Sub Specify()
        Function SetupWorld() As T
        Function Run() As Outcome

    End Interface


End Namespace
