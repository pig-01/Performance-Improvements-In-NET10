---
agent: 'agent'
tools: ['io.github.github/github-mcp-server/get_me', 'io.github.github/github-mcp-server/issue_read', 'io.github.github/github-mcp-server/list_issues', 'githubRepo']
description: 'List my issues in the current repository'
---

Search the current repo (using #githubRepo for the repo info) and list any issues you find (using #list_issues) that are assigned to me.

Suggest issues that I might want to focus on based on their age, the amount of comments, and their status (open/closed).
