Option Strict On

Namespace FluentInterface

    Public Interface IOutcome
        Function [And](ByVal description As String, ByVal outcome As World.IWorldOutcome) As IOutcome
    End Interface

End Namespace
