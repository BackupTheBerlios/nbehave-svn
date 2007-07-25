Option Strict On

Imports System.Collections.ObjectModel
Imports System.Collections.Generic

Namespace Scenario


    Public Class ScenarioEventArgs
        Inherits NBehaveEventArgs

        Private _scenario As Object


        Public ReadOnly Property Scenario() As Object
            Get
                Return _scenario
            End Get
        End Property


        Public Sub New(ByVal scenario As Object)
            Me.new(scenario, Nothing)
        End Sub


        Public Sub New(ByVal scenario As Object, ByVal outcome As Outcome)
            MyBase.New(outcome)
            Me._scenario = scenario
        End Sub
    End Class


End Namespace
