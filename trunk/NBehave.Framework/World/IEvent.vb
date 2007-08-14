Option Strict On

Namespace World

    Public Interface [IEvent]
        Sub OccurIn(Of T)(ByVal world As T)
    End Interface

End Namespace