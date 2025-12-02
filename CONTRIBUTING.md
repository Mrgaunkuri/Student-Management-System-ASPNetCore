# Contributing

- Branching model: use feature branches like `feat/<area>`, `fix/<ticket>`, `chore/<task>` — keeps history organized.
- Pull requests: open PRs to `main`, include a short description, testing steps, and link issues — speeds review.
- Commit messages: use imperative style `feat:`, `fix:`, `docs:`, `refactor:` and reference issue numbers — keeps history readable.
- Code style & formatting: follow repository `.editorconfig`; run `dotnet format` before committing — enforces consistency.
- Tests & CI: add unit tests under `tests/`; ensure GitHub Actions pass before requesting a merge — preserves stability.
- Security: never commit secrets; put credentials in environment variables or use GitHub Secrets for CI — prevents leaks.
