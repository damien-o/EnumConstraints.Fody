# ![EnumConstraints.Fody logo](https://raw.githubusercontent.com/damien-o/EnumConstraints.Fody/main/docs/package_icon_40.png) EnumConstraints.Fody

[![NuGet Version](https://img.shields.io/nuget/v/EnumConstraints.Fody.svg)](https://www.nuget.org/packages/EnumConstraints.Fody/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/EnumConstraints.Fody)](https://www.nuget.org/packages/EnumConstraints.Fody/)
[![.net Status](https://github.com/damien-o/EnumConstraints.Fody/actions/workflows/dotnet.yml/badge.svg)](https://github.com/damien-o/EnumConstraints.Fody/actions/)
[![Activity](https://img.shields.io/github/commit-activity/m/damien-o/EnumConstraints.Fody)](https://github.com/damien-o/EnumConstraints.Fody/pulse/)
[![Coverage](https://img.shields.io/badge/Code%20Coverage-97%25-success?style=flat)](https://github.com/damien-o/EnumConstraints.Fody/actions/)
[![CodeQL](https://github.com/damien-o/EnumConstraints.Fody/workflows/CodeQL/badge.svg)](https://github.com/damien-o/EnumConstraints.Fody/actions/workflows/github-code-scanning/codeql)
[![License](https://img.shields.io/github/license/damien-o/EnumConstraints.Fody)](https://github.com/damien-o/EnumConstraints.Fody/blob/main/LICENSE)
[![Chat on Gitter](https://img.shields.io/gitter/room/fody/fody.svg)](https://gitter.im/Fody/Fody)

Validates enum property value.

**See [Milestones](https://github.com/damien-o/EnumConstraints.Fody/milestones?state=closed) for release notes.**

### This is an add-in for [Fody](https://github.com/Fody/Home/)

**It is expected that all developers using Fody [become a Patron on OpenCollective](https://opencollective.com/fody/contribute/patron-3059). [See Licensing/Patron FAQ](https://github.com/Fody/Home/blob/master/pages/licensing-patron-faq.md) for more information.**

## Usage

See also [Fody usage](https://github.com/Fody/Home/blob/master/pages/usage.md).

### NuGet installation

Install the [EnumConstraints.Fody NuGet package](https://nuget.org/packages/EnumConstraints.Fody/) and update the [Fody NuGet package](https://nuget.org/packages/Fody/):

```powershell
PM> Install-Package Fody
PM> Install-Package EnumConstraints.Fody
```

The `Install-Package Fody` is required since NuGet always defaults to the oldest, and most buggy, version of any dependency.

### Add to FodyWeavers.xml

Add `<EnumConstraints/>` to [FodyWeavers.xml](https://github.com/Fody/Home/blob/master/pages/usage.md#add-fodyweaversxml)

```xml
<Weavers>
  <EnumConstraints/>
</Weavers>
```

## What it does

- Looks for all classes with Properties.
- Generates a new implementations of Set and Get methods.
- Replaces orginal implementations with the new ones.
- The new implementations are using the originals implementations under the hood

## Behavior

```cs
public enum Status
{
    Value1,
    Value2,
}

public class Sample
{
    public Status Status { get; set;}
}

var sample = new();

sample.Status = Status.Value1; // Valid

sample.Status = (Status)10; // Throws an InvalidEnumValueException
```

## How it works

### The orginal lowered code

```cs
public class Sample
{
    private Status <Status>k__BackingField;

    public Status Status
    {
        get; // Calls get_Status
        set; // Calls set_Status
    }

    public Status get_Status()
    {
        return <Status>k__BackingField;
    }
    public void set_Status(Status value)
    {
        <Status>k__BackingField = value;
    }
}
```

### What gets compiled

```cs
public class Sample
{
    private StringComparison <Status>k__BackingField;

    public Status Status
    {
        get; // Calls get_Status
        set; // Calls set_Status
    }

    public Status get_Status()
    {
        var value = get_Status_Unchecked();
        InvalidEnumValueException.ThrowIfInvalid(value);
        return value;
    }

    public void set_Status(Status value)
    {
        InvalidEnumValueException.ThrowIfInvalid(value);
        set_Status_Unchecked(value);
    }

    public Status get_Status_Unchecked
    {
        return <Status>k__BackingField;
    }
    public void set_Status_Unchecked(Status value)
    {
        <Status>k__BackingField = value;
    }
}
```

## Icon

[Checklist](https://www.onlinewebfonts.com/icon/464401) designed by [Web Fonts](http://www.onlinewebfonts.com).
