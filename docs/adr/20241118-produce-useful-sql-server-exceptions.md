# Produce useful SQL Server exceptions

- Status: approved
- Deciders: Daniel Mackay
- Date: 2024-11-18
- Tags: ef-core

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/408

## Context and Problem Statement

EF Core typically throws `DbUpdateException` which doesn’t tell you much until you drill into the inner exceptions.  If you do drill in you will find exceptions specific to the underlying DB Provider.  But what if you change provider?  Now you need to handle multiple exceptions for the same error.

## Decision Drivers

- Produce strongly typed useful exceptions

## Decision Outcome

Chosen option: "Option 1", because it does what we need and is far better than the default EF Core exceptions.

## Pros and Cons of the Options

### Option 1 - EntityFrameworkCore.Exceptions.SqlServer

- ✅ Strongly typed exceptions
- ❌ Additional dependency added
