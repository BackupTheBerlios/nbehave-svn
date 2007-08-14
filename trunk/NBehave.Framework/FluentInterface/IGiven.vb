Option Strict On

Namespace FluentInterface

    Public Interface IGivenStart
        Function Given(ByVal description As String, ByVal theGiven As World.IGiven) As IGiven
    End Interface

    Public Interface IGiven
        Function [And](ByVal description As String, ByVal theGiven As World.IGiven) As IGiven
        Function [When](ByVal description As String, ByVal theEvent As World.IEvent) As IEvent
    End Interface

End Namespace
