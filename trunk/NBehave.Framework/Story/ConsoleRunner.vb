Option Strict On

Namespace Story


    Public Class ConsoleRunner


        Public Enum ConsoleOutput
            Simple
            Text
        End Enum


        Dim runner As StreamRunner

        Public Sub New()
            Me.New(Reflection.Assembly.GetCallingAssembly)
        End Sub

        Public Sub New(ByVal assemblyToParseForStories As Reflection.Assembly)
            Me.New(assemblyToParseForStories, ConsoleOutput.Text)
        End Sub

        Public Sub New(ByVal assemblyToParseForStories As Reflection.Assembly, ByVal runnerType As ConsoleOutput)
            Select Case runnerType
                Case ConsoleOutput.Text : runner = New FullTextRunner(Console.OpenStandardOutput(), assemblyToParseForStories)
                Case ConsoleOutput.Simple : runner = New StreamRunner(Console.OpenStandardOutput(), assemblyToParseForStories)
                Case Else
                    runner = New StreamRunner(Console.OpenStandardOutput(), assemblyToParseForStories)
            End Select
        End Sub


        Public Sub Run()
            runner.Run()
        End Sub

    End Class


End Namespace
