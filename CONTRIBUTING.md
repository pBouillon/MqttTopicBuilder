# Contributing to this project

🎊 Do you want to help ? Awesome ! Thank you ! 🎉  

There is plenty of different ways to contribute to this repository. The 
following document will provide details on how to contribute. Of course
these are mostly guidelines and not rules, your jugement and experience is
what prevail the most, please feel free to bring your own ideas !

---

## Summary

- [What you probably should know](#what-you-probably-should-know)
- [Guidelines](#guidelines)
- [Style guide](#style-guide)
- [Contributing](#contributing)

---

## What you probably should know

Before diving in please take time to go through the structure of the project and to check
[what MQTT is](http://mqtt.org/).

No coding level or experience is required to contribute, what matters is the outcome of
your contribution. I will gladly help anyone struggling to do so if I can.

## Guidelines

> For all development and all interactions, please follow common sense and respect.
> 
> No insults, discrimination, political beliefs or anything that should not have its place among
> a sharing and helpful community as the one on GitHub will be tolerated.

📝 If you need help for anything related to this project, do not hesitate to ask !

In your pull request, do not hesitate to add your name in the `CONTRIBUTORS.md` file !

### Git usage

When working on an issue, fork this project and work on a specific issue. **No pull request**
**will be merged if it contains more than one issue resulution**.

In order to help reviewers, commits on a regular basis useful content and not the whole work once
there are hundreds of lines to review.

To keep your commits meaningful, please follow 
[those guidelines](https://github.com/pBouillon/git_tutorials/blob/master/methodology/commit_rules.md)
regarding the commit length, tense, structure, etc.

### GitHub usage

Avoid spamming people and/or excessive tagging, your contribution or request will be reviewed when
possible. However, if you are afraid of it being forgotten, do not hesitate to ask again once in a
while.

#### Pull request

When submitting a new **pull request**, please specify in its title a hint on what this is about.
You can mostly rely [on this list](https://github.com/pBouillon/git_tutorials/blob/master/methodology/emoji_commit_list.md)
but you can also add a short tag in the front of it.  

For example: `[Configuration] Migrate from .NET Core X.Y to X'.Y'` or `⚙️ Migrate from .NET Core X.Y to X'.Y'`.

> If the work you are submitting is not done yet, include `[WiP]` in the front of your title.

## Style guide

- Private attributes should start with an underscore
- In general, follow [the C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)
- Please keep methods alphabetically ordered

Not really a style but each logic added should be tested and each method/class should be documented.

After your contribution, please increase the version of the project as specified by
[the semantic versionning](https://semver.org/). In doubt, ask ! 😄

## Contributing

### Request a change

Wether it is an improvement, a request, a bug fix or anything else, all suggestions are welcomed
(but with no guarantee to be further implemented).

In order to do so, please create a [new issue](https://github.com/pBouillon/MqttTopicBuilder/issues/new)
**after** checking if no other existing issue is about the same thing.

Keep the title clear and short, write the body in Markdown and tag the new issue with the appropriate
label.

Regarding what your request is, please provide at least the following information:

<details>
<summary>
Improvement or request
</summary>
<p>
- What you think is missing
- Why do you think it is missing and has its place in this project
- Any resources to help to its development (documentation, PoC, existing sources, etc.)
</p>

<summary>
Bug report
</summary>
<p>
- Comprehensive and descriptive description of the bug
- Clear examples and steps to demonstrate this bug
- The expected outcome and the actual result
- If possible, the stack trace of the error
</p>
</details>

### Working on an existing issue

Firstly, please browse [the issues tab](https://github.com/pBouillon/MqttTopicBuilder/issues) and
click on the one you would like to work on.  

Then, please write a comment to warn that you would like to work on it, it will soon be assigned to
you if no one else is currently on this subject or you will be welcomed to work along people already
in charge of it.

You're all set ! You can now work on it, following [the guidelines](#guidelines).

> Please keep in mind that your addition to this project will change its core content,
> it is important to keep the code clean and the quality of it as good as possible.

### Filling your pull request

**First of all, please ensure that you are up to date with the current state of the `master` branch**

To submit your pull request, provide a clear and concise title of what it is about. Then, in its
body, detail each modification you brought to the project.

Please, be sure that the code is following [the guidelines](#guidelines) and that
[all checks](https://github.com/pBouillon/MqttTopicBuilder/actions) are passing.

If your PR contains any major changes and will update the package's usage, it would be great
if you could add a section about what updates should be made in [the wiki](https://github.com/pBouillon/MqttTopicBuilder/wiki).

You can then submit it and wait for its review and its possible comments until it is merged !
