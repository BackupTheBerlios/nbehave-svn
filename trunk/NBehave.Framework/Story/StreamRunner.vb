Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports system.Threading.Thread


Namespace Story

    Public Class StreamRunner
        Inherits Story.StoryRunner

        Private ReadOnly SummaryString As String = "Passed: {0}" & Environment.NewLine & "Failed: {1}"
        Protected Const FailedString As String = "Failed !"
        Protected Const PassedString As String = "Passed !"


        Private _storyCount As Int32
        Private _failCount As Int32

        Private failedStories As IList(Of Scenario.ScenarioOutcome)
        Private _outStream As IO.StreamWriter



        Public Sub New(ByVal outStream As IO.Stream)
            Me.OutStream = New IO.StreamWriter(outStream)
        End Sub

        Public Sub New(ByVal outStream As IO.Stream, ByVal stories As IList)
            MyBase.New(stories)
            Me.OutStream = New IO.StreamWriter(outStream)
        End Sub

        Public Sub New(ByVal outStream As IO.Stream, ByVal assemblyToParseForStories As Reflection.Assembly)
            MyBase.New(assemblyToParseForStories)
            Me.OutStream = New IO.StreamWriter(outStream)
        End Sub


        Protected ReadOnly Property Summary() As String
            Get
                Return SummaryString
            End Get
        End Property

        Protected Property OutStream() As IO.StreamWriter
            Get
                Return _outStream
            End Get
            Set(ByVal Value As IO.StreamWriter)
                _outStream = Value
            End Set
        End Property

        Protected Property StoryCount() As Int32
            Get
                Return _storyCount
            End Get
            Set(ByVal Value As Int32)
                _storyCount = Value
            End Set
        End Property

        Protected Property FailCount() As Int32
            Get
                Return _failCount
            End Get
            Set(ByVal Value As Int32)
                _failCount = Value
            End Set
        End Property


        Private Sub StreamRunnerRunStart(ByVal sender As Object, ByVal e As StoryEventArgs) Handles Me.RunStart
        End Sub

        Private Sub StreamRunnerBeforeStoryRun(ByVal sender As Object, ByVal e As StoryEventArgs) Handles Me.ExecutingStory
            StoryCount += 1
        End Sub

        Private Sub StreamRunnerAfterStoryRun(ByVal sender As Object, ByVal e As StoryEventArgs) Handles Me.StoryExecuted
            If e.Outcome.Passed Then
                OutStream.Write(".")
            Else
                failedStories.Add(e.Outcome)
                OutStream.Write("x")
                FailCount += 1
            End If
            OutStream.Flush()
        End Sub


        Private Sub StreamRunnerRunFinished(ByVal sender As Object, ByVal e As StoryEventArgs) Handles Me.RunFinished
            OutStream.WriteLine()
            WriteSummary()
            WriteFinalOutcome()
            OutStream.Flush()
        End Sub

        Protected Overridable Sub WriteFinalOutcome()
            If FailCount = 0 Then
                OutStream.WriteLine(PassedString)
            Else
                OutStream.WriteLine(FailedString)
                'TODO: Print all failed Storie's to stream.
            End If
        End Sub



        Protected Overridable Sub WriteSummary()
            OutStream.WriteLine(String.Format(CurrentThread.CurrentUICulture, SummaryString, StoryCount - FailCount, FailCount))
            OutStream.Flush()
        End Sub


    End Class


End Namespace