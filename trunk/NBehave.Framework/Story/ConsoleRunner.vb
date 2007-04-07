Option Strict On

Namespace Story


    Public Class ConsoleRunner
        Inherits StreamRunner


        Public Sub New()
            Me.New(Reflection.Assembly.GetCallingAssembly)
        End Sub

        Public Sub New(ByVal assemblyToParseForStories As Reflection.Assembly)
            MyBase.New(Console.OpenStandardOutput(), assemblyToParseForStories)
        End Sub


        Protected Overrides Sub WriteFinalOutcome()
            If FailCount = 0 Then
                Console.ForegroundColor = ConsoleColor.Green
                Console.Write(PassedString)
            Else
                Console.ForegroundColor = ConsoleColor.Red
                Console.Write(FailedString)
            End If
            Console.ResetColor()
        End Sub


    End Class


End Namespace
