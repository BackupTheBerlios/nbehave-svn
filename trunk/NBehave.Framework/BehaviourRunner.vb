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
            If t.IsClass AndAlso Not t.IsAbstract Then
                Dim baseType As Type = t.BaseType
                If baseType IsNot Nothing AndAlso t.IsSubclassOf(GetType(Behaviour)) Then ' baseType.GetGenericTypeDefinition.Equals(GetType(Behaviour)) Then
                    'If ClassHasBehaviourAttribute(t) Then
                    Dim i As Object = System.Activator.CreateInstance(t, True)
                    objectsToInspect.Add(i)
                    'End If
                End If
            End If
        Next
    End Sub

    'TODO: Search out methods directly in the constructor and only invoke them in the Run method below. add the stuff to Stories collection in base class

    Public Overrides Sub Run()
        OnRunStart(Nothing)
        For Each instance As Object In objectsToInspect
            'invoke all public methods
            'Invoke Setup before invoking any other methods
            Dim methods() As Reflection.MethodInfo = instance.GetType.GetMethods(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)

            For Each method As Reflection.MethodInfo In methods
                'At the moment the class must implement IStoryBase, and inherit from Behaviour 
                If instance.GetType.Equals(method.DeclaringType) AndAlso (Not method.Name.Equals("Setup", StringComparison.InvariantCultureIgnoreCase)) Then
                    InvokeSetup(instance)
                    SetupStoryAndScenarios(instance, method)
                    Dim storyOutcome As Outcome = RunStory(instance)
                End If
            Next
        Next
        OnRunFinished(Nothing)
    End Sub



    'Private Function ClassHasBehaviourAttribute(ByVal t As Type) As Boolean
    '    Dim behaviourAttributes() As Attribute = CType(t.GetCustomAttributes(GetType(BehaviourAttribute), True), Attribute())

    '    Return (behaviourAttributes.Length > 0)
    'End Function


    Private Sub InvokeSetup(ByVal instance As Object)
        Dim setupMethod As Reflection.MethodInfo = instance.GetType.GetMethod("Setup", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        If setupMethod IsNot Nothing Then setupMethod.Invoke(instance, Nothing)
    End Sub

    Private Sub SetupStoryAndScenarios(ByRef instance As Object, ByVal method As Reflection.MethodInfo)
        'Invoke the method to setup the story & its scenarios
        method.Invoke(instance, Nothing)
    End Sub


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
        OnScenarioExecuted(sender, e)
    End Sub


End Class
