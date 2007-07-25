Option Strict On

Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports NBehave.Framework.Scenario


Public Class NBehaveEventArgs
    Inherits EventArgs

    Private _outcome As Outcome


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal outcome As Outcome)
        _outcome = outcome
    End Sub

    Public ReadOnly Property Outcome() As Outcome
        Get
            Return _outcome
        End Get
    End Property

End Class
