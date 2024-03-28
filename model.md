# SuperHero Domain Model

- `Hero` - Aggregate
- `Team` - Aggregate
- `Power` - ValueObject
- `Mission` - Entity
- `HeroPowerUpdated` - Domain Event (updates the TotalPowerLevel on the SuperHeroTeam)

```mermaid
classDiagram
    class Hero {
        string name
        string alias
        int PowerLevel
        Power[] Powers
    }

    class Power {
        string powerName
        string description
    }

    class Team {
        string Name
        int TotalPowerLevel
        enum TeamStatus
        Mission[] Missions
        void AddHero()
        void RemoveHero()
        void ExecuteMission()
    }
    
    class Mission {
        int MissionId
        string Description
        enum MissionStatus
    }

    Hero --> Power: has many
    Team --> Hero: has many
    Team --> Mission: has many
```
