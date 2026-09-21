using System;
using System.ComponentModel;

namespace PoGoEncTool.Core;

[Serializable]
public sealed record PogoEntry : IComparable<PogoEntry>
{
    [Category("Dates")] public PogoDate? Start { get; set; }
    [Category("Dates")] public bool LocalizedStart { get; set; }
    [Category("Dates")] public PogoDate? End { get; set; }
    [Category("Dates")] public bool HasEndTolerance { get; set; }

    [Category("Detail")] public PogoShiny Shiny { get; set; }
    [Category("Detail")] public PogoGender Gender { get; set; }
    [Category("Detail")] public PogoType Type { get; set; }
    [Category("Detail")] public byte? MinIV { get; set; } = 0;
    [Category("Detail")] public byte? MinLevel { get; set; } = 1;
    [Category("Detail")] public PogoBallRestriction? BallRestriction { get; set; } = PogoBallRestriction.Poke_Great_Ultra_Master;
    [Category("Detail")] public bool IsGigantamax { get; set; }
    [Category("Detail")] public bool IsFeaturedGOWildArea { get; set; }

    // last property
    [Category("Misc")] public string Comment { get; set; } = string.Empty;

    public void CopyTo(PogoEntry other)
    {
        other.Start = Start;
        other.LocalizedStart = LocalizedStart;
        other.End = End;
        other.HasEndTolerance = HasEndTolerance;

        other.Shiny = Shiny;
        other.Gender = Gender;
        other.Type = Type;
        other.MinIV = MinIV;
        other.MinLevel = MinLevel;
        other.BallRestriction = BallRestriction;
        other.IsGigantamax = IsGigantamax;
        other.IsFeaturedGOWildArea = IsFeaturedGOWildArea;

        other.Comment = Comment;
    }

    public static PogoEntry CreateNew() => new()
    {
        Start = PogoDate.CreateNew(),
        End = null,
        Shiny = PogoShiny.Never,
        Gender = PogoGender.Random,
        Type = PogoType.Wild,
        LocalizedStart = true,
        HasEndTolerance = true,
    };

    public override string ToString()
    {
        var date = $"[{Start?.ToString() ?? "X"}-{End?.ToString() ?? "X"}]";
        var gender = Gender switch
        {
            PogoGender.Random => "",
            PogoGender.FemaleOnly => " (♀)",
            _ => " (♂)",
        };
        return $"{date}: {Type} {{{Shiny}}}{gender} - {Comment}";
    }

    public int CompareTo(PogoEntry? p)
    {
        if (p == null)
            return 1;

        if (p.Start != null)
        {
            if (Start == null)
                return 1;
            var date = Start.CompareTo(p.Start);
            if (date != 0)
                return date;
        }
        else
        {
            if (Start != null)
                return -1;
        }

        if (p.End != null)
        {
            if (End == null)
                return 1;
            var date = End.CompareTo(p.End);
            if (date != 0)
                return date;
        }
        else
        {
            if (End != null)
                return -1;
        }

        if (Type != p.Type)
            return Type.CompareTo(p.Type);

        if (Shiny != p.Shiny)
            return Shiny.CompareTo(p.Shiny);
        if (Gender != p.Gender)
            return Gender.CompareTo(p.Gender);

        return string.Compare(Comment, p.Comment, StringComparison.OrdinalIgnoreCase);
    }

    public bool EqualsNoComment(PogoEntry other)
    {
        if (ReferenceEquals(this, other)) return true;
        return Equals(Start, other.Start) && Equals(End, other.End) && Shiny == other.Shiny && Gender == other.Gender && Type == other.Type;
    }

    public void Clear()
    {
        Type = PogoType.None; // marked for removal, don't bother clearing other fields
    }

    public bool InitializeDefaultsForType(PogoType newType)
    {
        if (newType is PogoType.Egg)
        {
            MinIV = 1;
            BallRestriction = PogoBallRestriction.OnlyPoke;
            return true;
        }

        if (newType is PogoType.Egg12km)
        {
            MinIV = 1;
            MinLevel = 8;
            BallRestriction = PogoBallRestriction.OnlyPoke;
            return true;
        }

        if (newType is PogoType.Raid or PogoType.RaidShadow or PogoType.MaxBattle or PogoType.MaxBattleGigantamax)
        {
            MinIV = 1;
            MinLevel = 20;
            BallRestriction = PogoBallRestriction.OnlyPremier;

            if (newType is PogoType.MaxBattleGigantamax)
                IsGigantamax = true;

            return true;
        }

        if (newType is PogoType.RaidMythical or PogoType.MaxBattleMythical)
        {
            MinIV = 10;
            MinLevel = 20;
            BallRestriction = PogoBallRestriction.OnlyPremier;
            return true;
        }

        if (newType is PogoType.RaidUltraBeast or PogoType.RaidShadowUltraBeast or PogoType.MaxBattleUltraBeast)
        {
            MinIV = 1;
            MinLevel = 20;
            BallRestriction = PogoBallRestriction.OnlyBeast;
            return true;
        }

        if (newType is PogoType.RaidShadowMythical)
        {
            MinIV = 8;
            MinLevel = 20;
            BallRestriction = PogoBallRestriction.OnlyPremier;
            return true;
        }

        if (newType is >= PogoType.FieldResearch and <= PogoType.ReferralBonus)
        {
            MinIV = 1;
            MinLevel = 15;
            BallRestriction = PogoBallRestriction.Poke_Great_Ultra_Master;
            return true;
        }

        if (newType is PogoType.GBL)
        {
            MinIV = 1;
            MinLevel = 20;
            BallRestriction = PogoBallRestriction.Poke_Great_Ultra_Master;
            return true;
        }

        if (newType is PogoType.GBLMythical)
        {
            MinIV = 10;
            MinLevel = 20;
            BallRestriction = PogoBallRestriction.Poke_Great_Ultra_Master;
            return true;
        }

        if (newType is PogoType.GBLEvent)
        {
            MinIV = 0;
            MinLevel = 20;
            BallRestriction = PogoBallRestriction.Poke_Great_Ultra_Master;
            return true;
        }

        if (newType is PogoType.Shadow)
        {
            MinIV = 0;
            MinLevel = 8;
            BallRestriction = PogoBallRestriction.OnlyPremier;
            return true;
        }

        if (newType is PogoType.ShadowMythical)
        {
            MinIV = 8;
            MinLevel = 8;
            BallRestriction = PogoBallRestriction.OnlyPremier;
            return true;
        }

        if (newType is PogoType.ShadowUltraBeast)
        {
            MinIV = 8;
            MinLevel = 8;
            BallRestriction = PogoBallRestriction.OnlyBeast;
            return true;
        }

        return false;
    }
}
