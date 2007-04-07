Option Strict On

Namespace FluentInterface

    Public Interface IEvent(Of T)
        Function [Then](ByVal outcome As World.IWorldOutcome(Of T)) As IOutcome(Of T)
    End Interface

End Namespace
