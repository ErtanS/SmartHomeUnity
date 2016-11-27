public class Config
{
    //Server Adress
    public const string ip = "http://localhost";

    //Insert
    public const string URL_INSERT = ip + "/smart/insertDevice.php";
    public const string URL_INSERT_PLAYLIST = ip + "/smart/insertMusicPlaylist.php";

    //Select
    public const string URL_GET_ALL = ip + "/smart/selectAllUnity.php";

    //Update
    public const string URL_GET_TIME = ip + "/smart/getTime.php";
    public const string URL_UPDATE = ip + "/smart/update.php";
    public const string URL_UPDATE_ROOMNAME = ip + "/smart/renameRoom.php";
    public const string URL_RESET = ip + "/smart/resetUnity.php";
    public const string URL_UPDATE_TIMESTAMP = ip + "/smart/updateTimestampInDevices.php";

    //Delete
    public const string URL_DELETE_TIMESTAMPS = ip + "/smart/deleteAllTimestamps.php";
    public const string URL_CLEAR_TABLE = ip + "/smart/clearMusicPlaylist.php";
    public const string URL_DELETE = ip + "/smart/delete.php";
    public const string URL_DELETE_ROOM = ip + "/smart/deleteRoom.php";

    //Other DataBase Operations
    public const string URL_BACKUP_DATABASE = ip + "/smart/backupDatabase.php";
    public const string URL_CLEAR_DATABASE = ip + "/smart/clearDatabase.php";
    public const string URL_RESTORE_DATABASE = ip + "/smart/restoreDatabase.php";
    public const string URL_UPLOAD =ip+ "/smart/upload.php";

    //Push Notification
    public const string URL_PUSH_NOTIFICATION = ip + "/smart/firebase.php";
    public const string URL_EMERGENCY = ip + "/smart/emergency.php";

    //JSON Tags
    public const string TAG_JSON_ARRAY = "result";
    public const string TAG_JSON_ARRAY_LIGHT = "resultLight";
    public const string TAG_JSON_ARRAY_WINDOW = "resultWindow";
    public const string TAG_JSON_ARRAY_SPEAKER = "resultSpeaker";
    public const string TAG_JSON_ARRAY_SHUTTERS = "resultShutters";
    public const string TAG_JSON_ARRAY_HEATER = "resultHeater";
    public const string TAG_JSON_ARRAY_DOOR = "resultDoor";
    public const string TAG_JSON_ARRAY_CAMERA = "resultCamera";
    public const string TAG_JSON_ARRAY_DRYER = "resultDryer";
    public const string TAG_JSON_ARRAY_OVEN = "resultOven";
    public const string TAG_JSON_ARRAY_PC = "resultPc";
    public const string TAG_JSON_ARRAY_STOVE = "resultStove";
    public const string TAG_JSON_ARRAY_TV = "resultTv";
    public const string TAG_JSON_ARRAY_WALL = "resultWall";
    public const string TAG_JSON_ARRAY_WASHER = "resultWasher";
    public const string TAG_JSON_ARRAY_WATER = "resultWater";

    //Database row Tags
    public const string TAG_ID = "id";
    public const string TAG_HOUR = "hour";
    public const string TAG_MINUTE = "minute";
    public const string TAG_STATE = "state";
    public const string TAG_NAME = "name";
    public const string TAG_COLOR = "color";
    public const string TAG_INTENSITY = "intensity";
    public const string TAG_SCENARIOROOM = "scenarioRoom";
    public const string TAG_SONGID = "songid";
    public const string TAG_STOP = "stop";
    public const string TAG_PASSWORD = "password";
    public const string TAG_UPDATED = "updated";
    public const string TAG_VOLUME = "volume";
    public const string TAG_TEMPERATURE = "temperature";
    public const string TAG_CATEGORY = "category";
    public const string TAG_PICTUREID = "pictureid";
    public const string TAG_VIDEOID = "videoid";
    public const string TAG_DURATION = "duration";
    public const string TAG_AUTOEMERGENCY = "autoEmergency";
    public const string TAG_FREQUENCY = "frequency";
    public const string TAG_EMERGENCY = "emergency";
    public const string TAG_RPM = "rpm";
    public const string TAG_AMOUNT = "amount";
    public const string TAG_CLOTHES = "clothes";
    public const string TAG_CHANNEL = "channel";
    public const string TAG_MUSIC = "music";

    //Category Tags
    public const string STRING_CATEGORY_TIMESTAMP = "timestamp";
    public const string STRING_CATEGORY_DEVICE = "device";
    public const string STRING_CATEGORY_SCENARIO = "scenario";

    //Table Tags
    public const string TAG_TABLE_DOOR = "door";
    public const string TAG_TABLE_SPEAKER = "speaker";
    public const string TAG_TABLE_SHUTTERS = "shutters";
    public const string TAG_TABLE_LIGHT = "light";
    public const string TAG_TABLE_WINDOW = "window";
    public const string TAG_TABLE_HEATER = "heater";

    //Room Tags GER
    public const string STRING_GER_WOHNZIMMER = "Wohnzimmer";
    public const string STRING_GER_KUECHE = "Kueche";
    public const string STRING_GER_FLUR = "Flur";
    public const string STRING_GER_SCHLAFZIMMER = "Schlafzimmer";
    public const string STRING_GER_BAD = "Bad";
    public const string STRING_GER_KINDERZIMMER = "Kinderzimmer";
    public const string STRING_GER_GARAGE = "Garage";
    public const string STRING_GER_WASCHKUECHE = "Waschkueche";
    public const string STRING_GER_BUERO = "Buero";

    //Room Tags EN
    public const string STRING_EN_WOHNZIMMER = "LivingRoom";
    public const string STRING_EN_KUECHE = "Kitchen";
    public const string STRING_EN_FLUR = "Corridor";
    public const string STRING_EN_SCHLAFZIMMER = "BedRoom";
    public const string STRING_EN_BAD = "Bath";
    public const string STRING_EN_KINDERZIMMER = "Nursery";
    public const string STRING_EN_GARAGE = "Garage";
    public const string STRING_EN_WASCHKUECHE = "WashRoom";
    public const string STRING_EN_BUERO = "Office";

    //Tags
    public const string STRING_INTENT_ROOM = "room";
    public const string STRING_INTENT_TYPE = "type";
    public const int INT_STATUS_EIN = 1;
    public const int INT_STATUS_AUS = 0;
    public const string STRING_STATUS_EIN = "1";
    public const string STRING_STATUS_AUS = "0";
    public const string STRING_TAG_TEXTVIEW = "txt";
    public const string STRING_TAG_IMAGEVIEW = "arrow";
    public const string STRING_EMPTY = "";
    public const string STRING_SPACE = " ";
    public const string MATERIAL_TEXTURE = "_MainTex";
    public const string SET_COLOR = "_Color";

    //Select & Update TAGS
    public const string STRING_TABLE = "table";
    public const string STRING_COLUMN = "column";
    public const string STRING_WHERE_VALUE = "where_value";
    public const string STRING_WHERE_ROW = "where_row";
    public const string STRING_SET_COLUMN = "set_column";
    public const string STRING_SET_VALUE = "set_value";

    //Type TAGS
    public const string STRING_TYPE_EN_LIGHT = "light";
    public const string STRING_TYPE_GER_LIGHT = "Lampe";
    public const string STRING_TYPE_EN_SPEAKER = "speaker";
    public const string STRING_TYPE_GER_SPEAKER = "Lautsprecher";
    public const string STRING_TYPE_GER_DOOR = "Tuer";
    public const string STRING_TYPE_EN_DOOR = "door";
    public const string STRING_TYPE_GER_SHUTTERS = "Rollladen";
    public const string STRING_TYPE_EN_SHUTTERS = "shutters";
    public const string STRING_TYPE_EN_WINDOW = "window";
    public const string STRING_TYPE_GER_WINDOW = "Fenster";
    public const string STRING_TYPE_EN_HEATER = "heater";
    public const string STRING_TYPE_GER_HEATER = "Heizung";
    public const string STRING_TYPE_EN_CAMERA = "camera";
    public const string STRING_TYPE_EN_DRYER = "dryer";
    public const string STRING_TYPE_EN_OVEN = "oven";
    public const string STRING_TYPE_EN_PC = "pc";
    public const string STRING_TYPE_EN_STOVE = "stove";
    public const string STRING_TYPE_EN_TV = "tv";
    public const string STRING_TYPE_EN_WALL = "wall";
    public const string STRING_TYPE_EN_WASHER = "washer";
    public const string STRING_TYPE_EN_WATER = "water";
    public const string STRING_TYPE_GER_CAMERA = "Überwachung";
    public const string STRING_TYPE_GER_DRYER = "Trockner";
    public const string STRING_TYPE_GER_OVEN = "Ofen";
    public const string STRING_TYPE_GER_PC = "Computer";
    public const string STRING_TYPE_GER_STOVE = "Herd";
    public const string STRING_TYPE_GER_TV = "Fernseher";
    public const string STRING_TYPE_GER_WALL = "Wand";
    public const string STRING_TYPE_GER_WASHER = "Waschmachine";
    public const string STRING_TYPE_GER_WATER = "Wasser";

    public const string STRING_THIEF = "Thief";
    public const string STRING_TABLE_CLOCK = "clock";

    public const string STRING_MESSAGE_WASHER = "washer~";
    public const string STRING_MESSAGE_OVEN = "oven~";
    public const string STRING_MESSAGE_DRYER = "dryer~";
    public const string STRING_MESSAGE_STOVE = "stove~";
    public const string STRING_MESSAGE_CAMERA = "camera~";

    //Unity tags
    public const string STRING_MUSIC_FOLDER = "musik";
    public const string STRING_VIDEO_FOLDER = "video";
    public const string STRING_AUDIO_SOURCE = "music";
    public const string STRING_PREFAB_FOLDER = "prefab";
    public const string STRING_PICTURE_FOLDER = "picture";
    public const string STRING_ICON_FOLDER = "icons";
    public const double DOUBLE_STANDARD_TEMPERATURE = 15;
    public const float FLOAT_REFRESH_INTERVAL = 0.3f;
    public const int INT_UNSET_ID = -1;
    public const string STRING_NOT_IN_ROOM = "not_In_room";

    //prefabs
    public const string STRING_PREFAB_WALL = "WallCube";
    public const string STRING_PREFAB_CEILING = "CeilingCube";
    public const string STRING_PREFAB_EMPTY = "Empty";
    public const string STRING_PREFAB_FLOOR = "FloorCube";
    public const string STRING_PREFAB_KITCHEN_SINK = "kitchensink";
    public const string STRING_PREFAB_SINK = "sink";
    public const string STRING_PREFAB_POLE = "Pfosten";
    public const string STRING_PREFAB_TUB = "tub";
    public const string STRING_PREFAB_MAGICWALL = "magicWall";
    public const string STRING_PREFAB_MAGICWALL_FRONT = "Front";
    public const string PREFAB_GRID_CUBE = "GridCube";
    public const string STRING_BUTTON_DELETE = "delete";

    //GameObjects
    public const string STRING_GAMEOBJECT_CANVAS = "Canvas";
    public const string STRING_GAMEOBJECT_MODES = "modes";
    public const string STRING_GAMEOBJECT_MENU = "Menu";
    public const string STRING_GAMEOBJECT_CAMERATOP = "CameraTop";
    public const string STRING_GAMEOBJECT_MANAGER = "Manager";
    public const string STRING_GAMEOBJECT_CHARAKTER = "Charakter";
    public const string OBJ_CURRENT_ROOM = "currentRoom";
    public const string OBJ_NAME_POLE_MATERIAL = "Cube1u";
    public const string OBJ_NAME_PANEL = "panel";
    public const string OBJ_NAME_SWITCH = "schalter";
    public const string OBJ_NAME_CANVAS = "Canvas";
    public const string OBJ_NAME_TOGGLE = "Toggle";
    public const string OBJ_NAME_NUMBER = "number";
    public const string OBJ_NAME_TIMESPEED = "timeSpeed";
    public const string OBJ_NAME_RANDOM_PLAYER = "RandomPlayer";
    public const string OBJ_NAME_MESSAGE = "Message";
    public const string OBJ_NAME_START = "start";
    public const string OBJ_NAME_TARGET = "target";
    public const string OBJ_INPUT_NAME = "ipName";
    public const string OBJ_DOOR = "door";
    public const string OBJ_DOOR_CLOSE = "doorGlasClose";
    public const string OBJ_DOOR_OPEN = "doorGlas";
    public const string OBJ_PASSWORD_FIELD = "passwordField";
    public const string STRING_GAMEOBJECT_VIEW = "View";
    public const string STRING_COLLIDER = "collider";
    public const string STRING_ABOVE = "above";
    public const string STRING_BELOW = "below";
    public const string STRING_LEFT = "left";
    public const string STRING_RIGHT = "right";
    public const string STRING_CUBE = "cube";
    public const string STOVE_OR = "OR";
    public const string STOVE_UR = "UR";
    public const string STOVE_OL = "OL";
    public const string STOVE_UL = "UL";
    public const string STRING_LIGHT_SPOT = "Spot";
    public const string STRING_LIGHT_HALO = "Halo";
    public const string STRING_LAMPEN = "Lampen";
    public const string STRING_SPOT = "spot";
    public const string STRING_SPOTLIGHT = "SpotLight";
    public const string OBJ_SHUTTERS_ROLLO = "rollo";
    public const string OBJ_STOVE_PLATTE= "platte";
    public const string OBJ_WASHER_TROMMEL= "Trommel";
    public const string OBJ_WINDOW_FENSTER = "fenster";

    //Info messages - Current Device
    public const string MSG_DOOR = "Ausgewähltes Objekt: Tür\nEine Tür kann platziert werden, indem die Wand an der gewünschten Position ausgewählt wird.";
    public const string MSG_WINDOW = "Ausgewähltes Objekt: Fenster\nEin Fenster kann platziert werden, indem die Wand an der gewünschten Position ausgewählt wird.\nFenster können nur an Außenwände platziert werden.";
    public const string MSG_SHUTTERS = "Ausgewähltes Objekt: Rollladen\nRollladen können platziert werden, indem das Fenster an der gewünschten Position ausgewählt wird.\nAnschließend kann ein dazugehöriger Schalter platziert werden. Hierzu wird ein Pfosten an der gewünschten Position ausgewählt.\nUm nachträglich einen Schalter einzufügen, ist es möglich die gewünschten Rollladen auszuwählen, um anschließend wie beschrieben einen Schalter hinzuzufügen.";
    public const string MSG_SINK = "Ausgewähltes Objekt: Waschbecken\nEin Waschbecken kann platziert werden, indem die Wand an der gewünschten Position ausgewählt wird.";
    public const string MSG_MAGICWALL = "Ausgewähltes Objekt: Leinwand\nEine Leinwand kann platziert werden, indem die Wand an der gewünschten Position ausgewählt wird.\nNach dem Platzieren werden gegebenenfalls anliegende Leinwände mit der eingefügten Leinwand verbunden.";
    public const string MSG_CAMERA = "Ausgewähltes Objekt: Kamera\nEine Kamera kann platziert werden, indem der Boden oder ein Bodenobjekt unterhalb der gewünschten Position ausgewählt wird.\n";
    public const string MSG_LIGHT = "Ausgewähltes Objekt: Lampe\nEine Lampe kann an der Decke platziert werden, indem der Boden oder ein Bodenobjekt unterhalb der gewünschten Position ausgewählt wird.\nAnschließend kann ein dazugehöriger Schalter platziert werden. Hierzu wird ein Pfosten an der gewünschten Position ausgewählt.\nUm nachträglich einen Schalter einzufügen, ist es möglich die gewünschte Lampe auszuwählen, um anschließend wie beschrieben einen Schalter hinzuzufügen.";
    public const string MSG_SPEAKER = "Ausgewähltes Objekt: Lautsprecher\nEin Lautsprecher kann an der Decke platziert werden, indem der Boden oder ein Bodenobjekt unterhalb der gewünschten Position ausgewählt wird.\nAnschließend kann ein dazugehöriger Schalter platziert werden. Hierzu wird ein Pfosten an der gewünschten Position ausgewählt.\nUm nachträglich einen Schalter einzufügen, ist es möglich den gewünschten Lautsprecher auszuwählen, um anschließend wie beschrieben einen Schalter hinzuzufügen.";
    public const string MSG_DRYER = "Ausgewähltes Objekt: Trockner\nEin Trockner kann platziert werden, indem der Boden an der gewünschten Position ausgewählt wird.\nZum Rotieren wird ein bereits vorhandendes Objekt ausgewählt. Dieses wird daraufhin, wenn möglich um 90° gedreht.";
    public const string MSG_WASHER = "Ausgewähltes Objekt: Waschmaschine\nEine Waschmaschine kann platziert werden, indem der Boden an der gewünschten Position ausgewählt wird.\nZum Rotieren wird ein bereits vorhandendes Objekt ausgewählt. Dieses wird daraufhin, wenn möglich um 90° gedreht.";
    public const string MSG_KITCHENSINK = "Ausgewähltes Objekt: Spüle\nEine Spüle kann platziert werden, indem der Boden an der gewünschten Position ausgewählt wird.\nZum Rotieren wird ein bereits vorhandendes Objekt ausgewählt. Dieses wird daraufhin, wenn möglich um 90° gedreht.";
    public const string MSG_OVEN = "Ausgewähltes Objekt: Ofen\nEin Ofen kann platziert werden, indem der Boden an der gewünschten Position ausgewählt wird.\nZum Rotieren wird ein bereits vorhandendes Objekt ausgewählt. Dieses wird daraufhin, wenn möglich um 90° gedreht.";
    public const string MSG_PC = "Ausgewähltes Objekt: Computer\nEin Computer kann platziert werden, indem der Boden an der gewünschten Position ausgewählt wird.\nZum Rotieren wird ein bereits vorhandendes Objekt ausgewählt. Dieses wird daraufhin, wenn möglich um 90° gedreht.";
    public const string MSG_STOVE = "Ausgewähltes Objekt: Herd\nEin Herd kann platziert werden, indem der Ofen an der gewünschten Position ausgewählt wird.\nZum Rotieren wird ein bereits vorhandendes Objekt ausgewählt. Dieses wird daraufhin, wenn möglich um 90° gedreht.";
    public const string MSG_TUB = "Ausgewähltes Objekt: Badewanne\nEine Badewanne kann platziert werden, indem der Boden an der gewünschten Position ausgewählt wird.\nZum Rotieren wird ein bereits vorhandendes Objekt ausgewählt. Dieses wird daraufhin, wenn möglich um 90° gedreht.";
    public const string MSG_TV = "Ausgewähltes Objekt: Fernseher\nEin Fernseher kann platziert werden, indem der Boden an der gewünschten Position ausgewählt wird.\nZum Rotieren wird ein bereits vorhandendes Objekt ausgewählt. Dieses wird daraufhin, wenn möglich um 90° gedreht.";
    public const string MSG_DELETE = "Geräte löschen:\nUm ein Objekt zu löschen, wird dieses ausgewählt. Daraufhin verschwindet dieses Objekt unwiederruflich. Dieser Vorgang kann nicht rückgängig gemacht werden!";
    public const string MSG_SWITCH_ONLY_ON_POLE = "Schalter können nur in den markierten Bereichen platziert werden.";
    public const string MSG_PLACE_MODE = "In diesem Modus ist es möglich, Geräte im Haus zu platzieren.\nZur Steuerung der Kamera benutzen Sie die Pfeiltasten. Um die Kamera zu rotieren, nutzen Sie 'W','A','S','D'. Mit 'Bild Auf' und 'Bild Ab' können Sie die Höhe regulieren.\nZum Platzieren eines Gerätes wählen Sie den entsprechenden Typ aus der Liste der Buttons links am Bildrand aus.";
    public const string MSG_MENU = "Um ins Menü zu gelangen,\ndrücken Sie TAB.";

    //Error messages
    public const string MSG_DOOR_ON_WALL = "Türen können nur an Wänden platziert werden.";
    public const string MSG_WALL_CONTAINS_OBJECT = "An dieser Wand befindet sich bereits ein Objekt.";
    public const string MSG_CANNOT_PLACE_DOOR = "Tür konnte nicht platziert werden.";
    public const string MSG_WINDOW_ON_WALL = "Fenster können nur an Außenwänden platziert werden.";
    public const string MSG_CANNOT_PLACE_WINDOW = "Fenster konnte nicht platziert werden.";
    public const string MSG_SHUTTERS_ON_WINDOW = "Rollläden können nur an Fenstern platziert werden.";
    public const string MSG_WINDOW_CONTAINS_SHUTTERS = "An diesem Fenster befinden sich bereits Rollladen.";
    public const string MSG_CANNOT_PLACE_SHUTTERS = "Rollladen konnten nicht platziert werden.";
    public const string MSG_ONLY_ON_WALL = "Das ausgewählte Objekt kann nur an Wänden platziert werden.";
    public const string MSG_STOVE_ON_OVEN = "Ein Herd kann nur auf einem Ofen platziert werden.";
    public const string MSG_NOT_ENOUGHT_SPACE = "Das Objekt kann hier nicht platziert werden, da es mit anderen Objekten kollidieren würde.";
    public const string MSG_ONLY_ON_FLOOR = "Das ausgewählte Objekt kann nur auf dem Boden platziert werden.";
    public const string MSG_SWITCH_ON_POLE = "Um einen Schalter zu platzieren, müssen Sie auf einen Pfosten klicken.";
    public const string MSG_CEILINGOBJECTS_ON_FLOOR = "Deckenobjekte werden platziert, indem der Boden oder ein Bodenobjekt unterhalb der gewünschten Position ausgewählt wird.";
    public const string MSG_CAN_ONLY_DELETE_DEVICES = "Es können nur Geräte gelöscht werden.";
    public const string MSG_CANNOT_ROTATE = "Das Objekt kann nicht rotiert werden.";
    public const string MSG_INVALID_PASSWORD = "Passwort falsch!";
    public const string MSG_JOIN_WALLS = "Aneinanderliegende Wände wurden zusammengefügt.";
    public const string MSG_ERROR_ALREADY_HAS_SWITCH = "Auf dieser Position befindet sich bereits ein Schalter.";
    public const string MSG_THIEF_CAUGHT = "Der Dieb wurde gefangen.";
    public const string MSG_DEFAULT_GRID_MODE_TEXT = "In diesem Modus können Sie den Grundriss ihres Hauses erstellen. Hierbei werden die Räume einzeln erstellt. Um ein Raum zu errichten, wählen Sie die gewünschten Positionen auf dem sichtbaren Raster aus. Wenn Sie ihren Raum komplettiert haben, können Sie mit Hilfe des 'Raum erstellen' Buttons einen Namen für diesen Raum vergeben. Danach werden die hinzugehörigen Wände gezeichnet. Mit diesem Verfahren ist es möglich mehrere Räume zu erstellen. Um einen Raum umzubenennen, wählen Sie eine Bodenplatte innerhalb des Raumes mit der rechten Maustaste aus. Wenn Sie einen Raum wieder entfernen möchten, wählen Sie eine Bodenplatte in diesem Raum aus. Dies ist nur bei fertiggestellten Räumen möglich. Achtung: Dieser Schritt kann nicht rückgängig gemacht werden!";
    public const string MSG_CANNOT_DELETE_FLOOR = "Boden kann nicht gelöscht werden, da sonst der Raum nicht mehr zusammenhängt.";
    public const string MSG_CANNOT_DELETE_FLOOR_PLACEMODE = "Der Boden kann nicht gelöscht werden.";
    public const string MSG_CANNOT_DELETE_POLE= "Pfosten können nicht gelöscht werden.";
    public const string MSG_CANNOT_DELETE_WALL = "Wände können nicht gelöscht werden.";
    public const string MSG_ERROR_CANNOT_DELETE_ROOM = "Raum kann nicht gelöscht werden bevor der aktuelle Raum gesichert ist.";
    public const string MSG_ERROR_CANNOT_PLACE_FLOOR = "Diese Position hängt nicht mit dem Raum zusammen.";
    public const string MSG_ERROR_FIRST_NAME_ROOM = "Bitte geben Sie erst einen Namen für den Raum ein.";
    public const string MSG_ERROR_ENTER_NAME = "Bitte geben Sie einen Namen ein.";
    public const string MSG_ERROR_NAME_TAKEN = "Der eingegebene Name ist bereits vorhanden.";
    public const string MSG_NOT_ALLOWED_CHARACTERS = "Der Name enthält unerlaubte Zeichen.";
    public const string MSG_CANNOT_CHANGE_NAME = "Der Name konnte nicht geändert werden.";
    public const string MSG_ERROR_ROOMCOUNT_EQUALS_ZERO = "Bitte erstellen Sie zunächst einen Raum.";
    public const string MSG_ERROR_SAVE_ROOM_BEFORE = "Um die Datei speichern zu können, müssen Sie zunächst den aktuellen Raum sichern.";



    //Animations
    public const string ANIMATION_DOOR_OPEN_IDLE = "DoorOpenIdle";
    public const string ANIMATION_DOOR_CLOSE_IDLE = "DoorCloseIdle";
    public const string ANIMATION_DOOR_IDLE = "DoorIdle";
    public const string ANIMATION_TRIGGER_OPEN = "Open";
    public const string ANIMATION_TRIGGER_CLOSE = "Close";

    public const string ANIMATION_WASHER_ON = "WasherOn";
    public const string ANIMATION_WASHER_OFF = "WasherOff";
    public const string ANIMATION_TRIGGER_ON = "on";
    public const string ANIMATION_TRIGGER_OFF = "off";

    public const string ANIMATION_WATER_IDLE = "WasserIdle";
    public const string ANIMATION_WATER_IDLE_OFF = "WasserAusIdle";
    public const string ANIMATION_TRIGGER_WATER_OFF = "aus";
    public const string ANIMATION_TRIGGER_WATER_ON = "an";

    public const string ANIMATION_WINDOW_OPEN_IDLE = "fensterAufIdle";
    public const string ANIMATION_WINDOW_IDLE = "fensterIdle";
    public const string ANIMATION_TRIGGER_WINDOW_CLOSE = "close";
    public const string ANIMATION_TRIGGER_WINDOW_OPEN  = "open";

    public const string ANIMATION_SHUTTERS_UP_IDLE = "RolloUpIdle";
    public const string ANIMATION_SHUTTERS_IDLE= "RolloIdle";
    public const string ANIMATION_TRIGGER_UP = "Up";
    public const string ANIMATION_TRIGGER_DOWN = "Down";

    //Buttons
    public const string BTN_PLAY = "btnPlay";
    public const string BTN_BUILD_MODE = "btnBuildMode";
    public const string BTN_BUILD = "btnBuild";
    public const string BTN_NEW = "btnNew";
    public const string BTN_LOAD = "btnLoad";
    public const string BTN_APPLY = "btnApply";
    public const string BTN_CANCEL = "btnCancel";
    public const string BTN_APPLY_NAME = "btnApplyName";

    //Strings
    public const string SET_DIGITAL_FONT = "Font/digital";
    public const string STRING_OUTSIDE = "Draussen";
    public const string STRING_PREFIX_POS_TRANSFORM = "pos:";
    public const string STRING_BODY = "body";
    public const string STRING_THIEF_BEHAVIOUR = "ThiefBehaviour";
    public const string STRING_THIEF_CONTROLLER = "ThiefController";
    public const string STRING_THIEF_ROOM = "ThiefRoom";
    public const string STRING_LED = "led";
    public const string STRING_PLAYER = "Player";
    public const string STRING_UNDESTROYABLE = "undestroyable";
    public const string STRING_DOORHANDLE = "doorhandle";
    public const string STRING_WALL_X = "wallX";
    public const string STRING_FLOOR = "floor";
    public const string STRING_POLE = "pole";
    public const string STRING_INPUTFIELD_ROOMNAME = "raumbezeichnung";
}
