# SuperHero Domain Model

- `Hero` - Aggregate
- `Team` - Aggregate
- `Power` - ValueObject
- `Mission` - Entity
- `HeroPowerUpdated` - Domain Event (updates the TotalPowerLevel on the SuperHeroTeam)

```mermaid
classDiagram
    class Hero {
        string Name
        string Alias
        int Strength
        Power[] Powers
        void AddPower()
        void RemovePower()
    }

    class Power {
        string Name
        string Strength
    }

    class Team {
        string Name
        int TotalStrength
        enum TeamStatus
        Mission[] Missions
        void AddHero()
        void RemoveHero()
        void ExecuteMission()
        void CompleteCurrentMission()
    }
    
    class Mission {
        int MissionId
        string Description
        enum MissionStatus
        void Complete()
    }

    Hero --> Power: has many
    Team --> Hero: has many
    Team --> Mission: has many
```
## Database Schema

![SuperHero Database Schema](./database.png)
