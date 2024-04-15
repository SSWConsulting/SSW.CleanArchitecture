using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SSW.CleanArchitecture.Infrastructure.Audit;

public class AuditEntry
{
    public Guid Id { get; set; }
    public string TableName { get; set; } = null!;
    public AuditType AuditType { get; set; }
    public DateTimeOffset AuditedAt { get; set; }
    public string? UserId { get; set; }

    public string KeyValues { get; set; } = string.Empty;

    public string OldValues { get; set; } = string.Empty;

    public string NewValues { get; set; } = string.Empty;

    public string ChangedColumns { get; set; } = string.Empty;

    public AuditEntry()
    {
    }

    public static AuditEntry? From(EntityEntry entry, string? userId, DateTimeOffset now)
    {
        if (entry.State is EntityState.Detached or EntityState.Unchanged ||
            string.IsNullOrWhiteSpace(entry.Metadata.GetTableName()))
        {
            return null;
        }

        var auditEntry = new AuditEntry
        {
            TableName = entry.Metadata.GetTableName()!, UserId = userId, AuditedAt = now,
        };
        
        var keyValues = new Dictionary<string, object?>();
        var oldValues = new Dictionary<string, object?>();
        var newValues = new Dictionary<string, object?>();
        var changedColumns = new List<string>();

        foreach (var property in entry.Properties)
        {
            var propertyName = property.Metadata.Name;
            var dbColumnName = property.Metadata.GetColumnName();

            if (property.Metadata.IsPrimaryKey())
            {
                keyValues[propertyName] = property.CurrentValue;
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Added:
                    newValues[propertyName] = property.CurrentValue;
                    auditEntry.AuditType = AuditType.Create;
                    break;

                case EntityState.Deleted:
                    oldValues[propertyName] = property.OriginalValue;
                    auditEntry.AuditType = AuditType.Delete;
                    break;

                case EntityState.Modified:
                    if (property.IsModified)
                    {
                        changedColumns.Add(dbColumnName);

                        oldValues[propertyName] = property.OriginalValue;
                        newValues[propertyName] = property.CurrentValue;
                        auditEntry.AuditType = AuditType.Update;
                    }

                    break;
            }
        }
        
        auditEntry.KeyValues = JsonSerializer.Serialize(keyValues);
        auditEntry.OldValues = JsonSerializer.Serialize(oldValues);
        auditEntry.NewValues = JsonSerializer.Serialize(newValues);
        auditEntry.ChangedColumns = JsonSerializer.Serialize(changedColumns);

        return auditEntry;
    }
}

public enum AuditType
{
    None = 0,
    Create = 1,
    Update = 2,
    Delete = 3
}