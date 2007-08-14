Option Strict On

Namespace World

    Public Interface IGiven
        Sub Setup(Of T)(ByVal world As T)
    End Interface

End Namespace