

Namespace Scenario


    Public Class ScenarioOutcome
        Inherits Outcome

        Public Sub New()
            Me.new(False, String.Empty)
        End Sub

        Public Sub New(ByVal passed As Boolean, ByVal message As String)
            MyBase.New(passed, message)
        End Sub

    End Class



End Namespace