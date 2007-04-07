Option Strict On

Namespace World

    Public Interface IGiven(Of T)
        Sub Setup(ByVal world As T)
    End Interface

End Namespace