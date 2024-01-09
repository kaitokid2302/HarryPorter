using System.Collections.ObjectModel;
using PropertyChanged;
using System.Text.Json;
using System.ComponentModel;


namespace HarryPorter;

[AddINotifyPropertyChangedInterface]
public class HRViewModel : INotifyPropertyChanged
{
    public List<Question> Questions { get; set; } = new List<Question>()
{
    new Question("What is the eye color of Harry Potter?", "Green", "Blue", "Brown", "Grey", 1),
    new Question("Which house does Harry Potter belong to?", "Gryffindor", "Slytherin", "Hufflepuff", "Ravenclaw", 1),
    new Question("Who is the headmaster of Hogwarts during Harry's first year?", "Albus Dumbledore", "Severus Snape", "Minerva McGonagall", "Rubeus Hagrid", 1),
    new Question("What is the Patronus of Harry Potter?", "Stag", "Otter", "Doe", "Phoenix", 1),
    new Question("Who teaches Potions class in Harry's first year?", "Severus Snape", "Horace Slughorn", "Gilderoy Lockhart", "Remus Lupin", 1),
    new Question("What is the name of Harry Potter's owl?", "Hedwig", "Errol", "Crookshanks", "Scabbers", 1),
    new Question("What position does Harry play in Quidditch?", "Seeker", "Chaser", "Beater", "Keeper", 1),
    new Question("What is the last Horcrux to be destroyed?", "Nagini", "Harry Potter", "The Diadem of Ravenclaw", "The Cup of Hufflepuff", 1),
    new Question("Who destroyed the final Horcrux?", "Neville Longbottom", "Harry Potter", "Ron Weasley", "Hermione Granger", 1),
    new Question("What spell is used to open doors?", "Alohomora", "Expelliarmus", "Wingardium Leviosa", "Lumos", 1),
    new Question("What is the name of Ron's rat?", "Scabbers", "Hedwig", "Crookshanks", "Fang", 1),
    new Question("Who is the ghost of Gryffindor house?", "Nearly Headless Nick", "The Grey Lady", "The Bloody Baron", "Moaning Myrtle", 1),
    new Question("What is the effect of the Polyjuice Potion?", "Changes appearance", "Heals wounds", "Makes invisible", "Induces sleep", 1),
    new Question("What is the core of Harry Potter's wand?", "Phoenix feather", "Dragon heartstring", "Unicorn hair", "Veela hair", 1),
    new Question("Who is the Half-Blood Prince?", "Severus Snape", "Harry Potter", "Tom Riddle", "Albus Dumbledore", 1),
    new Question("What is the name of Hermione's cat?", "Crookshanks", "Hedwig", "Fang", "Scabbers", 1),
    new Question("What does the Marauder's Map show?", "Location of people", "Secret passages", "Future events", "Spell casting", 1),
    new Question("What house is Luna Lovegood in?", "Ravenclaw", "Gryffindor", "Slytherin", "Hufflepuff", 1),
    new Question("What is the name of Dumbledore's phoenix?", "Fawkes", "Buckbeak", "Nagini", "Aragog", 1)
};
    private static HRViewModel instance = null;
    public static HRViewModel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HRViewModel();
            }
            return instance;
        }
    }
    public ObservableCollection<Root> AllStudents { get; set; } = new ObservableCollection<Root>();
    public Root SelectedCharacter { get; set; } = new Root();
    
    public ObservableCollection<Root> AllStaff { get; set; } = new ObservableCollection<Root>();
    public ObservableCollection<Root> AllCharacters { get; set; } = new ObservableCollection<Root>();
    private ObservableCollection<Root> _allCharactersList;

    public ObservableCollection<Root> AllCharactersList
    {
        get => _allCharactersList;
        set
        {
            if (_allCharactersList != value)
            {
                _allCharactersList = value;
                OnPropertyChanged(nameof(AllCharactersList));
            }
        }
    }

    public async Task<List<Root>> getAllCharacters()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://hp-api.onrender.com/api/characters");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var characters = JsonSerializer.Deserialize<List<Root>>(content);
            return characters;
        }
        else
        {
            return null;
        }

    }

    public ObservableCollection<string> AllHouses { get; set; } = new ObservableCollection<string>();

    public HRViewModel()
    {
        Init();
    }

    async Task Init()
    {
        var path = FileSystem.AppDataDirectory;
        var fullPath = Path.Combine(path, "characters.json");
        if (!File.Exists(fullPath))
        {
            var characters = await getAllCharacters();
            var json = JsonSerializer.Serialize(characters);
            File.WriteAllText(fullPath, json);
            foreach (var character in characters)
            {
                // if character image is null, then it will be: "https://upload.wikimedia.org/wikipedia/commons/thumb/3/35/Orange_question_mark.svg/450px-Orange_question_mark.svg.png?20220903225041"
                if(character.image == null || character.image == "")
                {
                    character.image = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/35/Orange_question_mark.svg/450px-Orange_question_mark.svg.png?20220903225041";
                }
                AllCharacters.Add(character);
            }

            foreach (var c in AllCharacters)
            {
                // if any attribute is null, then it will be: "Unknown"
                // iterate through all attributes

                if (c.actor == null || c.actor == "")
                {
                    c.actor = "Unknown";
                }

                if (c.alive == null || c.alive == false)
                {
                    c.alive = false;
                }

                if (c.ancestry == null || c.ancestry == "")
                {
                    c.ancestry = "Unknown";
                }

                if (c.dateOfBirth == null || c.dateOfBirth == "")
                {
                    c.dateOfBirth = "Unknown";
                }

                if (c.species == null || c.species == "")
                {
                    c.species = "Unknown";
                }
            }
            AllStudents = new ObservableCollection<Root>(AllCharacters.Where(x => x.hogwartsStudent == true));
            AllStaff = new ObservableCollection<Root>(AllCharacters.Where(x => x.hogwartsStaff == true));
            AllCharactersList = new ObservableCollection<Root>(AllCharacters);
        }
        else
        {
            var json = File.ReadAllText(fullPath);
            var characters = JsonSerializer.Deserialize<List<Root>>(json);
            foreach (var character in characters)
            {
                if(character.image == null || character.image == "")
                {
                    character.image = "questionmark.png";
                }
                AllCharacters.Add(character);
            }
            foreach (var c in AllCharacters)
            {
                // if any attribute is null, then it will be: "Unknown"
                // iterate through all attributes

                if (c.actor == null || c.actor == "")
                {
                    c.actor = "Unknown";
                }

                if (c.alive == null || c.alive == false)
                {
                    c.alive = false;
                }

                if (c.ancestry == null || c.ancestry == "")
                {
                    c.ancestry = "Unknown";
                }

                if (c.dateOfBirth == null || c.dateOfBirth == "")
                {
                    c.dateOfBirth = "Unknown";
                }

                if (c.species == null || c.species == "")
                {
                    c.species = "Unknown";
                }
            }
            AllStudents = new ObservableCollection<Root>(AllCharacters.Where(x => x.hogwartsStudent == true));
            AllStaff = new ObservableCollection<Root>(AllCharacters.Where(x => x.hogwartsStaff == true));
            AllCharactersList = new ObservableCollection<Root>(AllCharacters);
            
        }
        SelectedCharacter = AllStudents[0];
        // get all houses
        foreach (var character in AllCharacters)
        {
            if (character.house != null && character.house != "" && !AllHouses.Contains(character.house))
            {
                AllHouses.Add(character.house);
            }
        }
        Console.WriteLine(AllCharactersList.Count);
        
    }
    public async Task Display(string textSearch)
    {
        var filteredCharacters = await Task.Run(() =>
        {
            var tempList = new List<Root>();

            if (textSearch == null || textSearch == "")
            {
                tempList = new List<Root>(AllCharacters);
            }
            else
            {
                textSearch = textSearch.ToLower();
                foreach (var character in AllCharacters)
                {
                    var name = character.name.ToLower();
                    var house = character.house.ToLower();
                    var actor = character.actor.ToLower();
                    var species = character.species.ToLower();
                    var patronus = character.patronus.ToLower();
                    var hairColor = character.hairColour.ToLower();
                    var eyeColor = character.eyeColour.ToLower();

                    if (name.Contains(textSearch) || house.Contains(textSearch) || actor.Contains(textSearch) || species.Contains(textSearch)  || hairColor.Contains(textSearch) || eyeColor.Contains(textSearch))
                    {
                        tempList.Add(character);
                    }
                }
            }

            return tempList;
        });
        // clear all characters
        MainThread.BeginInvokeOnMainThread(() =>
        {
            AllCharactersList = new ObservableCollection<Root>(filteredCharacters);
            // update on property changed
            OnPropertyChanged("AllCharactersList");
        });
        
    }
    // on property changed
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}