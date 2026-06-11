---
title: picture-frame repo analysis
status: doing
owner: @code_junior
updated: 2026-06-06
tags: [picture-frame, repo-analysis, build]
---

# picture-frame repo analysis

Hermes Kanban: t_bebdbf5a

## Build entry point
- Solution file: `MediaHub.sln`
- Build command: `cd /home/lancer1977/code/picture-frame && dotnet build MediaHub.sln`

## Notes
- The root solution includes the buildable .NET projects for the API, shared contracts, and the persistence test project.
- The Angular scaffold is tracked in the solution as a solution folder item so the repo has a single visible entry point from the repository root.
- The API persists subscriptions and media items to JSON files under `App_Data/` so channel state survives process restarts during local development.
