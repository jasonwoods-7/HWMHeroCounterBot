using Discord.Interactions;
using EnumFastToStringGenerated;

namespace HeroWars.Hero.Counter.Bot.Data;

[FastToString]
public enum Hero
{
    Alvanor,
    //Amira,
    Andvari,
    Arachne,
    Artemis,
    Astaroth,
    [ChoiceDisplay("Astrid and Lucas")]
    AstridAndLucas,
    Aurora,
    Celeste,
    Chabba,
    Cleaver,
    Cornelius,
    Corvus,
    Dante,
    Daredevil,
    [ChoiceDisplay("Dark Star")]
    DarkStar,
    Dorian,
    Elmir,
    Faceless,
    Fox,
    Galahad,
    Ginger,
    Heidi,
    Helios,
    Isaac,
    Ishmael,
    Jet,
    Jhu,
    Jorgen,
    Judge,
    [ChoiceDisplay("K'arkh")]
    Karkh,
    Kai,
    Keira,
    //Krista,
    //Lars,
    Lian,
    Lilith,
    Luther,
    Markus,
    Martha,
    Maya,
    Mojo,
    Morrigan,
    Nebula,
    Orion,
    Peppy,
    Phobos,
    [ChoiceDisplay("Qing Mao")]
    QingMao,
    Rufus,
    Satori,
    Sebastian,
    Thea,
    Tristan,
    Twins,
    //[ChoiceDisplay("Xe'Sha")]
    //XeSha,
    Yasmine,
    Ziri
}

public record Document
(
    string Id,
    HeroRecord Hero,
    Counters Counters
)
{
    public Document()
        : this(string.Empty, new HeroRecord(), new Counters())
    {
    }
}

public record HeroRecord
(
    string Name
)
{
    public HeroRecord()
        : this(string.Empty)
    {
    }
}

public record Counters
(
    IReadOnlyList<string> Hard,
    IReadOnlyList<string> Soft
)
{
    public Counters()
        : this(Array.Empty<string>(), Array.Empty<string>())
    {
    }
}
