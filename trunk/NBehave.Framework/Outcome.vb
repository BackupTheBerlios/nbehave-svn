Option Strict On

Public Class Outcome
    Private _passed As Boolean
    Private _message As String

    Public Property Passed() As Boolean
        Get
            Return _passed
        End Get
        Set(ByVal Value As Boolean)
            _passed = Value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal Value As String)
            _message = Value
        End Set
    End Property


    Public Sub New(ByVal passed As Boolean, ByVal message As String)
        Me._passed = passed
        Me._message = message
    End Sub
End Class
