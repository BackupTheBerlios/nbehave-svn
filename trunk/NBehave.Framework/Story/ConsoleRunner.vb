Option Strict On

Namespace Story


    Public Class ConsoleRunner

        Dim runner As StreamRunner

        Public Sub New()
            Me.New(Reflection.Assembly.GetCallingAssembly)
        End Sub

        Public Sub New(ByVal assemblyToParseForStories As Reflection.Assembly)
            runner = New FullTextRunner(Console.OpenStandardOutput(), assemblyToParseForStories)
        End Sub


        Public Sub Run()
            runner.Run()
            WriteFinalOutcome()
        End Sub

        Protected Sub WriteFinalOutcome()
            If runner.FailCount = 0 Then
                Console.ForegroundColor = ConsoleColor.Green
                Console.Write(StreamRunner.PassedString)
            Else
                Console.ForegroundColor = ConsoleColor.Red
                Console.Write(StreamRunner.FailedString)
            End If
            Console.ResetColor()
        End Sub


    End Class


End Namespace
