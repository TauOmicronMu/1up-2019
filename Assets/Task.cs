using System;
using System.Collections.Generic;

[System.Serializable]
public class Task {
    public int progress;
    public int length;
    public List<Task> nextTasks;
    public string label;
    public bool isMilestone;

    public List<int> boosts;
    public List<bool> boostsGlobal;

    public bool special;
    public Fixes fixes;

    // public Dictionary<string, HashSet<string>> cumulative;

    /*
     * Yeah lol this probs works hahahahahahh :^ )
     *       ~ Tom, 9:30am
     *
    public void traverse(string curr, List<Task> next, string prev) {
        if(!prev.Equals("")) this.cumulative[prev].Add(curr);
        foreach (var task in next) {
            traverse(task.label, task.nextTasks, curr);
        }
    }
    */

    public bool IsComplete() {
        return progress >= length;
    }

    public void Update(int tickSize) {
        this.progress += tickSize;
    }

    public bool Contains(Task t) {
        if (t == null) return false;
        if (t.label == null) return false;
        if (t.label.Equals(this.label)) return true;

        if (this.nextTasks == null) return false;
        
        bool contains = false;
        foreach (var task in this.nextTasks) {
            contains = contains || task.Contains(t);
        }

        return contains;
    }

    public Task(bool isMilestone, List<Task> nextTasks, string label, int length) {
        this.isMilestone = isMilestone;
        this.nextTasks = nextTasks;
        this.length = length;
        this.label = label;
        this.progress = 0;

        this.boosts = null;
        this.boostsGlobal = null;
        this.special = false;
        
        // traverse(this.label, this.nextTasks, "");
    }

    /*
     * Used for the breakable objects on the table - boosts is
     * in the format [hygeine, sanity, energy, foodness].
     * 
     * isGlobal corresponds to these, and is whether the boost is applied to
     * the single player, or to all players.
     */
    public Task(string label, int length, List<int> boosts, List<bool> boostsGlobal, Fixes fixes) {
        this.label = label;
        this.length = length;
        this.progress = 0;
        this.boosts = boosts;
        this.boostsGlobal = boostsGlobal;
        this.isMilestone = false;
        this.nextTasks = null;
        this.special = true;
        this.fixes = fixes;
    }

    public static Task CreateTaskTree() {
        Task AAARelease = new Task(true, null, "AAA Release", 50000);

        Task DLC = new Task(false, new List<Task>() {AAARelease}, "DLC", 30000);
        Task DRM = new Task(false, new List<Task>() {AAARelease}, "DRM", 30000);
        Task Microtransactions = new Task(false, new List<Task>() {AAARelease}, "Microtransactions", 30000);

        Task AARelease = new Task(true, new List<Task>() {DLC, DRM, Microtransactions}, "AA Release", 30000);

        Task BespokeSoundtrack = new Task(false, new List<Task>() {AARelease}, "BespokeSoundtrack", 20000);
        Task DataLakeStorageSolutions = new Task(false, new List<Task>() {AARelease}, "Data Lake Storage Solutions", 20000);
        Task AndroidDevelopers = new Task(false, new List<Task>() {AARelease}, "Android Developers", 20000);
        Task Blockchain = new Task(false, new List<Task>() {AARelease}, "Blockchain", 20000);
        Task CloudComputingSynergy = new Task(false, new List<Task>() {AARelease}, "Cloud Computing Synergy", 20000);
        Task NewAgeNetworkCore = new Task(false, new List<Task>() {AARelease}, "New-Age Network Core", 20000);
        Task RobotComposers = new Task(false, new List<Task>() {BespokeSoundtrack}, "Robot Composers", 20000);
        Task ArtificialCreativity = new Task(false, new List<Task>() {RobotComposers}, "Artificial Creativity", 20000);
        Task EmotionalExpressionCore = new Task(false, new List<Task>() {ArtificialCreativity}, "Emotional Expression Core", 20000);
        Task RobotNeuralArchitectures = new Task(false, new List<Task>() {EmotionalExpressionCore, AndroidDevelopers}, "Robot Neural Architectures", 20000);
        Task LogicCore = new Task(false, new List<Task>() {AndroidDevelopers, Blockchain}, "Logic Core", 20000);
        Task VLANsAndLinkAggregation = new Task(false, new List<Task>() {NewAgeNetworkCore, CloudComputingSynergy}, "VLANs and Link Aggregation", 20000);
        Task ThreeDGameEngine = new Task(false, new List<Task>() {NewAgeNetworkCore}, "3D Game Engine", 20000);
        
        Task BetaRelease = new Task(true, new List<Task>() {AARelease}, "Beta Release", 20000);
        
        Task DeepNeuralNetworks = new Task(false, new List<Task>() {RobotNeuralArchitectures, DataLakeStorageSolutions}, "Deep Neural Networks", 12000);
        Task AdvancedRobotics = new Task(false, new List<Task>() {RobotNeuralArchitectures}, "Advanced Robotics", 12000 );
        Task AutomatedMusicComposition = new Task(false, new List<Task>() {ArtificialCreativity}, "Automated Music Composition", 12000);
        Task DifferentialMovement = new Task(false, new List<Task>() {AdvancedRobotics}, "Differential Movement", 12000);
        Task CalculusStudies = new Task(false, new List<Task>() {DeepNeuralNetworks, DifferentialMovement}, "Calculus Studies", 12000);
        Task ImprovedComposition = new Task(false, new List<Task>() {AutomatedMusicComposition}, "Improved Composition", 12000);
        Task AutomatedSFXGeneration = new Task(false, new List<Task>() {AutomatedMusicComposition}, "Automated SFX Generation", 12000);
        Task MusicTechnology = new Task(false, new List<Task>() {ImprovedComposition, AutomatedSFXGeneration}, "Music Technology",12000);
        Task ProcedurallyGeneratedLevels = new Task(false, new List<Task>() {AndroidDevelopers, BetaRelease}, "Procedurally Generated Levels", 12000);
        Task Animation = new Task(false, new List<Task>() {ThreeDGameEngine}, "3D Game Engine", 12000);
        Task ComplexGameLogic = new Task(false, new List<Task>() {Animation, BetaRelease, LogicCore, ProcedurallyGeneratedLevels}, "Complex Game Logic", 12000);
        Task ClientSideInterpolation = new Task(false, new List<Task>() {VLANsAndLinkAggregation}, "Client-Side Interpolation", 12000);
        Task Cameras = new Task(false, new List<Task>() {BetaRelease, ThreeDGameEngine}, "Cameras", 12000);
        Task ImprovedNetworking = new Task(false, new List<Task>() {ClientSideInterpolation, BetaRelease}, "Improved Networking", 12000);
        Task ThreeDModels = new Task(false, new List<Task>() {Animation}, "3D Models", 12000);
        Task BasicRobotics = new Task(false, new List<Task>() {DifferentialMovement}, "Basic Robotics", 12000);
        Task ProceduralGeneration = new Task(false, new List<Task>() {ProcedurallyGeneratedLevels, AutomatedMusicComposition}, "Procedural Generation", 12000);
        Task Entropy = new Task(false, new List<Task>() {ComplexGameLogic, AutomatedSFXGeneration}, "Entropy", 12000);
        Task GameTheory = new Task(false, new List<Task>() {ComplexGameLogic, BasicRobotics}, "Game Theory", 12000);
        Task AdvancedMechanics = new Task(false, new List<Task>() {BasicRobotics}, "Advanced Mechanics", 12000);
        
        Task Prototype = new Task(true, new List<Task>() {Cameras, ClientSideInterpolation, ComplexGameLogic}, "Prototype", 12000);
        
        Task ImprovedSFXCreation = new Task(false, new List<Task>() {AutomatedSFXGeneration}, "Improved SFX Creation", 5000);
        Task FastFourierTransforms = new Task(false, new List<Task>() {MusicTechnology}, "Fast Fourier Transforms", 5000);
        Task BasicAudioEngineering = new Task(false, new List<Task>() {FastFourierTransforms}, "Basic Audio Engineering", 5000);
        Task PseudoRealisticSFX = new Task(false, new List<Task>() {ImprovedSFXCreation}, "Pseudo-Realistic SFX", 5000);
        Task FourierTransforms = new Task(false, new List<Task>() {BasicAudioEngineering}, "Fourier Transforms", 5000);
        Task SFXTheory = new Task(false, new List<Task>() {FourierTransforms, PseudoRealisticSFX}, "SFX Theory", 5000);
        Task SFXCreation = new Task(false, new List<Task>() {SFXTheory}, "SFX Creation", 5000);
        Task ComprehensiveMusicTheory = new Task(false, new List<Task>() {MusicTechnology}, "Comprehensive Music Theory", 5000);
        Task RenaissanceExposition = new Task(false, new List<Task>() {ComprehensiveMusicTheory}, "Renaissance Exposition", 5000);
        Task MedievalStudies = new Task(false, new List<Task>() {RenaissanceExposition}, "Medieval Studies", 5000);
        Task BaroqueTheory = new Task(false, new List<Task>() {MedievalStudies}, "Baroque Theory", 5000);
        Task SimpleComposition = new Task(false, new List<Task>() {BaroqueTheory, FourierTransforms}, "Simple Compositions", 5000);
        Task SimulationTheory = new Task(false, new List<Task>() {BasicRobotics, ProceduralGeneration, CalculusStudies, Entropy}, "Simulation Theory", 5000);
        Task TemperedAIOpponent = new Task(false, new List<Task>() {GameTheory}, "Tempered AI Opponent", 5000);
        Task ProbabilityTheory = new Task(false, new List<Task>() {TemperedAIOpponent, SimulationTheory}, "Probability Theory", 5000);
        Task PerfectAIOpponent = new Task(false, new List<Task>() {TemperedAIOpponent, Prototype}, "Perfect AI Opponent", 5000);
        Task Textures = new Task(false, new List<Task>() {ThreeDModels}, "Textures", 5000);
        Task Shadows = new Task(false, new List<Task>() {ThreeDModels}, "Shadows", 5000);
        Task BasicLightingEffects = new Task(false, new List<Task>() {ThreeDModels}, "Basic Lighting Effects", 5000);
        Task Flash = new Task(false, new List<Task>() {BasicLightingEffects}, "Flash", 5000);
        Task NetworkedSimplePrototype = new Task(false, new List<Task>() {ImprovedNetworking}, "Networked Simple Prototype", 5000);
        Task SimplePrototype = new Task(false, new List<Task>() {NetworkedSimplePrototype}, "Simple Prototype", 5000);
        Task SimulatedAnnealing = new Task(false, new List<Task>() {PerfectAIOpponent, SimulationTheory}, "Simulated Annealing", 5000);
        Task HillClimbing = new Task(false, new List<Task>() {SimulatedAnnealing}, "Hill Climbing", 5000);
        Task AStar = new Task(false, new List<Task>() {PerfectAIOpponent}, "AStar", 5000);
        Task BasicHeuristics = new Task(false, new List<Task>() {AStar, HillClimbing, ProbabilityTheory}, "Basic Heuristics", 5000);
        Task BinarySearch = new Task(false, new List<Task>() {BasicHeuristics}, "Binary Search", 5000);
        Task Quicksort = new Task(false, new List<Task>() {PerfectAIOpponent}, "Quicksort", 5000);
        Task DivideAndConquer = new Task(false, new List<Task>() {BinarySearch, Quicksort}, "Divide & Conquer", 5000);
        Task DijkstrasAlgorithm = new Task(false, new List<Task>() {BasicHeuristics}, "Dijkstra's Algorithm", 5000);
        Task TwoDGraphicsEngine = new Task(false, new List<Task>() {SimplePrototype}, "2D Graphics Engine", 5000);
        Task BasicGameLogic = new Task(false, new List<Task>() {ComplexGameLogic, SimplePrototype, AdvancedMechanics}, "Basic Game Logic", 5000);
        Task InitialGameIdea = new Task(false, new List<Task>() {BasicGameLogic, PerfectAIOpponent}, "Initial Game Idea", 5000);
        Task NewtonianPhysics = new Task(false, new List<Task>() {BasicGameLogic}, "Newtonian Physics", 5000);
        Task WebGL = new Task(false, new List<Task>() {ImprovedNetworking}, "WebGL", 5000);
        Task OpenGLBindings = new Task(false, new List<Task>() {WebGL, Textures, Shadows, TwoDGraphicsEngine}, "OpenGL Bindings", 5000);
        Task DynamicWebpage = new Task(false, new List<Task>() {WebGL}, "Dynamic Webpage", 5000);
        Task ClientServer = new Task(false, new List<Task>() {DynamicWebpage, NetworkedSimplePrototype}, "Client-Server Architecture", 5000);
        Task StaticWebpage = new Task(false, new List<Task>() {DynamicWebpage, Flash}, "Static Webpage", 5000);
        Task CSS = new Task(false, new List<Task>() {StaticWebpage}, "CSS", 5000);
        Task HTML = new Task(false, new List<Task>() {StaticWebpage}, "HTML", 5000);
        
        Task CreativeAutonomy = new Task(true, new List<Task>() {SFXCreation, SimpleComposition, DivideAndConquer, NewtonianPhysics, InitialGameIdea, OpenGLBindings, CSS, HTML}, "Creative Autonomy", 5000);
        
        Task Scratch = new Task(false, new List<Task>() {Flash}, "Scratch", 2000);
        Task TwoDGameWindow = new Task(false, new List<Task>() {CreativeAutonomy}, "2D Game Window", 2000);
        Task Pinball = new Task(false, new List<Task>() {CreativeAutonomy}, "Pinball", 2000);
        Task Sockets = new Task(false, new List<Task>() {ClientServer}, "Sockets", 2000);
        Task Ping = new Task(false, new List<Task>() {Sockets}, "Ping", 2000);
        Task Minesweeper = new Task(false, new List<Task>() {Pinball}, "Minesweeper", 2000);
        Task SimpleGraphicsEngine = new Task(false, new List<Task>() {TwoDGameWindow}, "Simple Graphics Engine", 2000);
        Task ASCIIArt = new Task(false, new List<Task>() {SimpleGraphicsEngine}, "ASCII Art Graphics", 2000);
        Task JavaFX = new Task(false, new List<Task>() {TwoDGameWindow}, "JavaFX", 2000);
        Task SequentialSearch = new Task(false, new List<Task>() {DijkstrasAlgorithm}, "Sequential Search", 2000);
        Task SixteenBitMusic = new Task(false, new List<Task>() {CreativeAutonomy}, "16-Bit Music", 2000);
        Task EightBitMusic = new Task(false, new List<Task>() {SixteenBitMusic}, "8-Bit Music", 2000);
        Task BasicSFX = new Task(false, new List<Task>() {EightBitMusic, Pinball}, "Basic SFX", 2000);
        Task JavaAWT = new Task(false, new List<Task>() {JavaFX}, "Java AWT", 2000);
        Task CommandLine = new Task(false, new List<Task>() {JavaAWT, ASCIIArt}, "Command-Line Interface", 2000);
        Task BubbleSort = new Task(false, new List<Task>() {SequentialSearch}, "Bubble Sort", 2000);
        Task TicTacToe = new Task(false, new List<Task>() {Minesweeper, SimpleGraphicsEngine}, "Tic-Tac-Toe", 2000);
        Task Pong = new Task(false, new List<Task>() {TicTacToe}, "Pong", 2000);
        
        Task ControlStructures = new Task(true, new List<Task>() {Scratch, Pong, Ping, CommandLine, BubbleSort, BasicSFX}, "Control Structures", 2000);

        Task InvisiTask = new Task(false, new List<Task>() {ControlStructures}, "", 0);

        return InvisiTask;
    }
}
