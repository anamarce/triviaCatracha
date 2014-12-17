

   //Copiado de http://answers.unity3d.com/questions/152035/printtogui.html
    static var myLog : String;
    private var output : String = "";
    private var stack : String = "";
    public var debugConsole: boolean = false;
    public var ancho : float = 100.0;
    public var alto : float = 100.0;
    public var posx : float = 300.0;
    public var posy : float = 300.0;
    
    
     
    function Awake () {
    
    
       
       Application.RegisterLogCallback(HandleLog);
       Debug.Log("iniciando Log");
    }
    
     
    function OnDestroy  () {
    // Remove callback when object goes out of scope
   
        Application.RegisterLogCallback(null);
    }
     
    function HandleLog (logString : String, stackTrace : String, type : LogType) {
    output = logString;
    stack = stackTrace;
    myLog  = "\n"+output + myLog;
    }
     
    function OnGUI () {
      
         
       if(debugConsole)
           myLog = GUI.TextArea (Rect (posx, posy, ancho, alto), myLog);
        debugConsole = GUI.Toggle(Rect(posx, posy-15, 100, 20), debugConsole, "Log");
    }
