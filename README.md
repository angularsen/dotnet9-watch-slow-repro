## Repro for slow dotnet watch with Blazor on .NET9
Related:
- [hot reload takes too much time · Issue \#47179 · dotnet/sdk](https://github.com/dotnet/sdk/issues/47179)
- [dotnet 9, "dotnet watch" is really bad in Blazor Server · Issue \#45810 · dotnet/sdk](https://github.com/dotnet/sdk/issues/45810)

## Problem
1. `dotnet watch` is slower on net9 than net8, in particular the _first_ change can take 10-20 seconds vs a few seconds for a trivial change to `.razor` files in our large Blazor Server app. (1)
2. Blazor seems to do a page reload after hot reload in our app, but have not yet been able to create a repro of this behavior in these sample apps. See details below.

(1)
In 9.0.2 SDK release, the hot reload was 10-20x slower for every change, but in 9.0.3 SDK release only the first hot reload is 10-20x slow and subsequent changes are fast.<br>
`9.0.104`, `9.0.202` and `9.0.300-preview.0.25174.3` all exhibit the slow first hot reload, and in our app, also a special page reload.<br>
In our app, these versions also do a special page reload upon hot reload. Details below.

## To run this sample
Run both projects in two separate shells:
```sh
dotnet watch --verbose --project Blazor8
```

```sh
dotnet watch --verbose --project Blazor9
```

- Wait for browser to launch for each project.
- Edit `Components/Pages/Home.razor` in either project, and change the title text or something trivial.

## Observations in template apps
- Simulating a slow `OnInitializedAsync` with Task.Delay, delays the hot reload on both (probably by design)

## Observations in our large Blazor Server app
- On net9, hot reload incurs what seems like a special page reload. The circuit stays connected and no new HTTP GET request to Blazor, but the page is pre-rendered again, and all scoped services are recreated and re-initialized.
- This page load is slow in our app and delays hot reload, and this did not happen in net8 SDK.

![image](https://github.com/user-attachments/assets/5e88dc7e-bc29-441c-bffe-eb4dcc8cbb36)

![image](https://github.com/user-attachments/assets/088547ae-ae77-4e85-a92d-4411c6136305)
