using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace System.Data.SQLiteEFCore
{
    /// <summary>
    ///     The <see cref="DiagnosticSource" /> event payload for
    ///     <see cref="SqliteEventId.UnexpectedConnectionTypeWarning" />.
    /// </summary>
    public class UnexpectedConnectionTypeEventData : EventData
    {
        /// <summary>
        ///     Constructs the event payload.
        /// </summary>
        /// <param name="eventDefinition"> The event definition. </param>
        /// <param name="messageGenerator"> A delegate that generates a log message for this event. </param>
        /// <param name="connectionType"> The connection type. </param>
        public UnexpectedConnectionTypeEventData(
            [NotNull] EventDefinitionBase eventDefinition,
            [NotNull] Func<EventDefinitionBase, EventData, string> messageGenerator,
            [NotNull] Type connectionType)
            : base(eventDefinition, messageGenerator)
        {
            ConnectionType = connectionType;
        }

        /// <summary>
        ///     The connection type.
        /// </summary>
        public virtual Type ConnectionType { get; }
    }
}
