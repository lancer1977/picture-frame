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
- Solution file: `src/MediaHub.sln`
- Build command: `cd /home/lancer1977/code/picture-frame && dotnet build src/MediaHub.sln`

## Notes
- The solution includes the buildable .NET projects for the API and shared contracts.
- The Angular scaffold is tracked in the solution as a solution folder item so the repo has a single visible entry point from `src/`.
