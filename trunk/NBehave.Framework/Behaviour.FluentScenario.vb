Option Strict On

Public Delegate Sub Action(Of T)(ByVal value As T)


Public Interface IScenarioGiven
    Function [Given](Of T)(ByVal description As String) As IScenarioGivenAnd
    Function [Given](Of T)(ByVal description As String, ByVal value As T) As IScenarioGivenAnd
    Function [Given](Of T)(ByVal description As String, ByVal value As T, ByVal theGiven As Action(Of T)) As IScenarioGivenAnd
End Interface

Public Interface IScenarioGivenAnd
    Function [And](Of T)(ByVal description As String) As IScenarioGivenAnd
    Function [And](Of T)(ByVal description As String, ByVal value As T) As IScenarioGivenAnd
    Function [And](Of T)(ByVal description As String, ByVal value As T, ByVal given As Action(Of T)) As IScenarioGivenAnd
    Function [When](Of T)(ByVal description As String, ByVal value As T) As IScenarioWhen
    Function [When](Of T)(ByVal description As String, ByVal value As T, ByVal [event] As Action(Of T)) As IScenarioWhen
End Interface

Public Interface IScenarioWhen
    Function [Then](Of T)(ByVal description As String, ByVal value As T) As IScenarioThen
    Function [Then](Of T)(ByVal description As String, ByVal value As T, ByVal validator As Action(Of T)) As IScenarioThen
End Interface

Public Interface IScenarioThen
    Function [And](Of T)(ByVal description As String, ByVal value As T) As IScenarioThen
    Function [And](Of T)(ByVal description As String, ByVal value As T, ByVal validator As Action(Of T)) As IScenarioThen
End Interface



Partial Public Class Behaviour



    Private Class FluentScenario
        Implements IScenarioGiven
        Implements IScenarioGivenAnd
        Implements IScenarioWhen
        Implements IScenarioThen


        Private scenario As Scenario_

        Public Sub New(ByVal scenario As Scenario_)
            Me.scenario = scenario
        End Sub


        Private Function ValueArray(ByVal value As Object) As Object()
            Dim values() As Object = {value}
            Return values
        End Function


        Private Function Given(Of T)(ByVal description As String, ByVal value As T, ByVal theGiven As Action(Of T)) As IScenarioGivenAnd Implements IScenarioGiven.Given, IScenarioGivenAnd.And
            scenario.Givens.Add(New Given_(description, ValueArray(value), theGiven))
            Return Me
        End Function

        Public Function Given(Of T)(ByVal description As String) As IScenarioGivenAnd Implements IScenarioGiven.Given, IScenarioGivenAnd.And
            Return Given(description, "", Nothing)
        End Function

        Public Function Given(Of T)(ByVal description As String, ByVal value As T) As IScenarioGivenAnd Implements IScenarioGiven.Given, IScenarioGivenAnd.And
            Return Given(description, value, Nothing)
        End Function


        Private Function [When](Of T)(ByVal description As String, ByVal value As T, ByVal [event] As Action(Of T)) As IScenarioWhen Implements IScenarioGivenAnd.When
            scenario.Event = New Event_(description, ValueArray(value), [event])
            Return Me
        End Function

        Public Function [When](Of T)(ByVal description As String, ByVal value As T) As IScenarioWhen Implements IScenarioGivenAnd.When
            Return [When](description, value, Nothing)
        End Function


        Private Function [Then](Of T)(ByVal description As String, ByVal expected As T, ByVal validator As Action(Of T)) As IScenarioThen Implements IScenarioWhen.Then, IScenarioThen.And
            scenario.Outcomes.Add(New Outcome_(description, ValueArray(expected), validator))
            Return Me
        End Function


        Public Function [Then](Of T)(ByVal description As String, ByVal value As T) As IScenarioThen Implements IScenarioWhen.Then, IScenarioThen.And
            Return [Then](description, value, Nothing)
        End Function


    End Class


End Class
