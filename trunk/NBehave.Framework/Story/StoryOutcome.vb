Option Strict On

Namespace Story

    Public Class StoryOutcome
        Inherits Outcome

        Private _scenarioOutcomes As Collections.Generic.IList(Of Scenario.ScenarioOutcome) = New Generic.List(Of Scenario.ScenarioOutcome)


        Public Sub New(ByVal outcomes() As Scenario.ScenarioOutcome)
            MyBase.New(False, "No outcomes exists")
            AddScenarioOutcomes(outcomes)
        End Sub


        Private Sub AddScenarioOutcomes(ByVal outcomes() As Scenario.ScenarioOutcome)
            If outcomes IsNot Nothing Then
                If outcomes.Length > 0 Then Me.Passed = True
                For Each o As Scenario.ScenarioOutcome In outcomes
                    Me.Passed = Me.Passed And o.Passed
                    If Not o.Passed Then Me.Message &= o.Message & Environment.NewLine
                    _scenarioOutcomes.Add(o)
                Next
                If Me.Passed Then Me.Message = String.Empty
            End If
        End Sub

        Public ReadOnly Property ScenarioOutcomes() As Generic.IList(Of Scenario.ScenarioOutcome)
            Get
                Return New Collections.ObjectModel.ReadOnlyCollection(Of Scenario.ScenarioOutcome)(_scenarioOutcomes)
            End Get
        End Property


    End Class

End Namespace
