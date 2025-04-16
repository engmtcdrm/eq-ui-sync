using System.IO;
using System.Text.RegularExpressions;

namespace EQUISync
{


    public class UIFile
    {
	    private string path;
	    private string characterName = string.Empty;
        private string serverName = string.Empty;

        public UIFile(string path)
	    {
            this.path = path;
            parseUIFile();
	    }

        private void parseUIFile()
        {
            // UI_<character name>_<server>.proj.ini
            // UI_<character name>_<server>.ini
            Regex regex = new Regex(@"UI_(?<name>[a-zA-Z]+)_(?<server>[a-zA-Z]+)?\.");
            string fileName = System.IO.Path.GetFileName(path);
            Match match = regex.Match(fileName);

            if (match.Success)
            {
                characterName = match.Groups["name"].Value;
                serverName = match.Groups["server"].Value;
                return;
            }

            throw new FormatException($"WARNING: [Ignoring File]: The filename '{fileName}' does not match the expected UI file format. Expected format: UI_CharacterName_Server*.ini.");
        }

        /// <summary>
        /// Gets the path to the UI file.
        /// </summary>
        public string Path
        {
            get { return path; }
        }

        /// <summary>
        /// Gets the character name from the UI file.
        /// </summary>
        public string CharacterName
        {
            get { return characterName; }
        }

        /// <summary>
        /// Gets the server name from the UI file.
        /// </summary>
        public string ServerName
        {
            get { return serverName; }
        }

        /// <summary>
        /// Gets the full server and character name in the format "Server - Character".
        /// </summary>
        public string ServerCharacterName
        {
            get {  return serverName + " - " + characterName; }
        }

        public override string ToString()
        {
            return ServerCharacterName; // Or any other property you want to display
        }
    }
}
