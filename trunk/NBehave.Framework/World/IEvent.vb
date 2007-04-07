Option Strict On

Namespace World

    Public Interface [IEvent](Of T)
        Sub OccurIn(ByVal world As T)
    End Interface

End Namespace