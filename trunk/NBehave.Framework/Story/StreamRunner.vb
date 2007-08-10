Option Strict On

Imports System.Collections.ObjectModel
Imports NBehave.Framework.Scenario
Imports NBehave.Framework.Utility
Imports system.Threading.Thread


Namespace Story

    Public Class StreamRunner
        Inherits StoryRunnerBase


        Private ReadOnly SummaryString As String = "Passed: {0}" & Environment.NewLine & "Failed: {1}"
        Public Const FailedString As String = "Failed !"
        Public Const PassedString As String = "Passed !"


        Private _storyCount As Int32
        Private _failCount As Int32

        Private failedStories As New List(Of Outcome)
        Private _outStream As IO.StreamWriter

        Private WithEvents storyRunner As StoryRunner
        Private WithEvents behaviourRunner As BehaviourRunner


        Public Sub New(ByVal outStream As IO.Stream, ByVal assemblyToParseForStories As Reflection.Assembly)
            Me._outStream = New IO.StreamWriter(outStream)
            storyRunner = New StoryRunner(assemblyToParseForStories)
            behaviourRunner = New BehaviourRunner(assemblyToParseForStories)
        End Sub


        Public Overrides Sub Run()
            If storyRunner.Stories.Count > 0 Then storyRunner.Run()
            behaviourRunner.Run()
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

        Public Property FailCount() As Int32
            Get
                Return _failCount
            End Get
            Protected Set(ByVal Value As Int32)
                _failCount = Value
            End Set
        End Property


        Protected Overridable Sub StreamRunnerRunStart(ByVal sender As Object, ByVal e As NBehaveEventArgs) Handles storyRunner.RunStart, behaviourRunner.RunStart
        End Sub


        Protected Overridable Sub StreamRunnerRunFinished(ByVal sender As Object, ByVal e As NBehaveEventArgs) Handles storyRunner.RunFinished, behaviourRunner.RunFinished
            OutStream.WriteLine()
            WriteSummary()
            WriteFinalOutcome()
            If FailCount > 0 Then WriteFailures()
            OutStream.Flush()
        End Sub


        Protected Overridable Sub StreamRunnerBeforeStoryRun(ByVal sender As Object, ByVal e As StoryEventArgs) Handles storyRunner.ExecutingStory, behaviourRunner.ExecutingStory
            StoryCount += 1
        End Sub


        Protected Overridable Sub StreamRunnerAfterStoryRun(ByVal sender As Object, ByVal e As StoryEventArgs) Handles storyRunner.StoryExecuted, behaviourRunner.StoryExecuted
            WriteResultAfterStoryRun(e.Outcome)
            If e.Outcome.Result = OutcomeResult.Failed Then
                failedStories.Add(e.Outcome)
                FailCount += 1
            End If
        End Sub


        Protected Overridable Sub StreamRunnerScenarioExecuted(ByVal sender As Object, ByVal e As NBehaveEventArgs) Handles storyRunner.ScenarioExecuted, behaviourRunner.ScenarioExecuted
            OutStream.Write("   " & GetDescription(sender))
            WriteOutcome(e.Outcome)
            OutStream.WriteLine()
        End Sub


        Private Function GetDescription(ByVal sender As Object) As String
            Dim description As String = String.Empty

            If GetType(IScenarioBase).IsAssignableFrom(sender.GetType) Then
                description = CType(sender, IScenarioBase).Title
            Else
                description = CamelCaseToNormalSentence(sender.GetType.Name)
            End If
            Return description
        End Function



        Protected Sub WriteOutcome(ByVal outcome As Outcome)
            Select Case outcome.Result
                Case OutcomeResult.Passed : OutStream.Write(" --> Passed")
                Case OutcomeResult.Failed : OutStream.Write(String.Format("  --> Failed - {0}", outcome.Message))
                Case OutcomeResult.Pending : OutStream.Write("  --> Pending")
                Case Else
                    Throw New NotImplementedException(String.Format("outcome {0} isn't implemented", outcome.ToString))
            End Select
        End Sub


        Protected Overridable Sub WriteResultAfterStoryRun(ByVal outcome As Outcome)
            Select Case outcome.Result
                Case OutcomeResult.Passed : OutStream.Write(".")
                Case OutcomeResult.Failed : OutStream.Write("x")
                Case OutcomeResult.Pending : OutStream.Write("p")
                Case Else
                    Throw New NotImplementedException(String.Format("outcome {0} isn't implemented", outcome.ToString))
            End Select
            OutStream.Flush()
        End Sub


        Protected Overridable Sub WriteFinalOutcome()
            If FailCount = 0 Then
                OutStream.WriteLine(PassedString)
            Else
                OutStream.WriteLine(FailedString)
            End If
        End Sub


        Protected Overridable Sub WriteSummary()
            OutStream.WriteLine(String.Format(CurrentThread.CurrentUICulture, SummaryString, StoryCount - FailCount, FailCount))
            OutStream.Flush()
        End Sub


        Protected Overridable Sub WriteFailures()
            OutStream.WriteLine()
            For Each failure As Outcome In failedStories
                OutStream.WriteLine(failure.Message)
            Next
            OutStream.Flush()
        End Sub


    End Class


End Namespace