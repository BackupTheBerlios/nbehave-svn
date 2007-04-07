Option Strict On

Namespace FluentInterface

    Public Interface IGiven(Of T)
        Function [And](ByVal theGiven As World.IGiven(Of T)) As IGiven(Of T)
        Function [When](ByVal theEvent As World.IEvent(Of T)) As IEvent(Of T)
    End Interface

End Namespace
