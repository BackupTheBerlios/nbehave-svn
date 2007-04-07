Option Strict On

Namespace FluentInterface

    Public Interface IOutcome(Of T)
        Function [And](ByVal outcome As World.IWorldOutcome(Of T)) As IOutcome(Of T)
    End Interface

End Namespace
