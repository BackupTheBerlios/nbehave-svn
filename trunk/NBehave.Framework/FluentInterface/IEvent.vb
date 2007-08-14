Option Strict On

Namespace FluentInterface

    Public Interface IEvent
        Function [Then](ByVal description As String, ByVal outcome As World.IWorldOutcome) As IOutcome
    End Interface

End Namespace
