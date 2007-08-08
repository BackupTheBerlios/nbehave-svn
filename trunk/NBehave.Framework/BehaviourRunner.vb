Option Strict On

Imports NBehave.Framework.Story
Imports System.Collections.ObjectModel

Public Class BehaviourRunner
    Inherits StoryRunnerBase



    Public Delegate Sub Story()
    Private objectsToInspect As IList(Of Object) = New List(Of Object)
    Private scenarioOutcomes As ReadOnlyCollection(Of Outcome)
    Private WithEvents storyToExecute As IStoryBase

    Public Sub New()
        Me.New(Reflection.Assembly.GetCallingAssembly)
    End Sub

    Public Sub New(ByVal assemblyToParseForStories As Reflection.Assembly)
        If assemblyToParseForStories Is Nothing Then Throw New ArgumentException("assemblyToParseForStories is NULL")

        For Each t As Type In assemblyToParseForStories.GetTypes()
            If t.IsClass AndAlso Not t.IsAbstract AndAlso t.BaseType.GetGenericTypeDefinition.Equals(GetType(Behaviour)) Then
                'If ClassHasBehaviourAttribute(t) Then
                Dim i As Object = System.Activator.CreateInstance(t, True)
                objectsToInspect.Add(i)
                'End If
            End If
        Next
    End Sub


    'Private Function ClassHasBehaviourAttribute(ByVal t As Type) As Boolean
    '    Dim behaviourAttributes() As Attribute = CType(t.GetCustomAttributes(GetType(BehaviourAttribute), True), Attribute())

    '    Return (behaviourAttributes.Length > 0)
    'End Function


    Public Overrides Sub Run()
        OnRunStart(Nothing)
        For Each instance As Object In objectsToInspect
            'invoke all public methods
            'Invoke Setup before invoking any other methods
            Dim methods() As Reflection.MethodInfo = instance.GetType.GetMethods(Reflection.BindingFlags.Public)

            For Each method As Reflection.MethodInfo In methods
                'At the moment the class must implement IStoryBase, and inherit from Behaviour 
                InvokeSetup(instance)
                SetupStoryAndScenarios(instance, method)
                Dim storyTitle As String = GetStoryTitle(instance)
                Dim storyOutcome As Outcome = RunStory(instance)
            Next
        Next
        OnRunFinished(Nothing)
    End Sub


    Private Sub InvokeSetup(ByVal instance As Object)
        Dim setupMethod As Reflection.MethodInfo = instance.GetType.GetMethod("Setup")
        If setupMethod IsNot Nothing Then setupMethod.Invoke(instance, Nothing)
    End Sub

    Private Sub SetupStoryAndScenarios(ByRef instance As Object, ByVal method As Reflection.MethodInfo)
        'Invoke the method to setup the story & its scenarios
        method.Invoke(instance, Nothing)
    End Sub

    Private Function GetStoryTitle(ByVal instance As Object) As String
        Dim storyTitleProperty As Reflection.PropertyInfo = instance.GetType.GetProperty("StoryTitle")
        Dim storyTitle As String = CType(storyTitleProperty.GetValue(instance, Nothing), String)
        Return storyTitle
    End Function

    Private Function RunStory(ByVal instance As Object) As Outcome
        storyToExecute = CType(instance, IStoryBase)
        Dim narrative As Narrative = storyToExecute.Narrative
        OnExecutingStory(New StoryEventArgs(storyToExecute))
        Dim storyOutcome As Outcome = storyToExecute.Run
        OnStoryExecuted(New StoryEventArgs(instance, storyOutcome))

        Return storyOutcome
    End Function



    Private Sub storyToExecute_ScenarioOutcome(ByVal sender As Object, ByVal e As NBehaveEventArgs) Handles storyToExecute.ScenarioOutcome
        scenarioOutcomes = e.Outcome.Outcomes
        'RaiseEvent ScenarioExecuted(sender, e)
    End Sub


End Class
