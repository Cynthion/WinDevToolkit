using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation;

namespace WPDevToolkit
{
    public static class BaseCalendar
    {
        private static bool _initialized;
        private static AppointmentStore _appointmentStore;
        private static AppointmentCalendar _currentAppCalendar;

        #region WinRT API

        // https://marcominerva.wordpress.com/2013/03/21/how-to-expose-async-methods-in-a-windows-runtime-component/
        // We need to follow precise rules when we develop a Windows Runtime Component.
        // One of the most important is that we have restrictions on the types that can be exposed.
        // For example, we can’t have public methods that return Task.

        public static IAsyncAction InitializeCalendarAsync()
        {
            return InitializeCalendarImpl().AsAsyncAction();
        }

        public static IAsyncOperation<string> CreateAppointmentAsync(Appointment appointment)
        {
            return CreateAppointmentImpl(appointment).AsAsyncOperation();
        }

        public static IAsyncAction DeleteAppointmentAsync(string localId)
        {
            return DeleteAppointmentImpl(localId).AsAsyncAction();
        }

        public static IAsyncAction ModifyAppointmentAsync(string localId)
        {
            return ModifyAppointmentImpl(localId).AsAsyncAction();
        }

        #endregion

        /// <summary>
        /// Initializes the calendar. If it is the application's first run, the calendar is created.
        /// </summary>
        /// <returns></returns>
        private static async Task InitializeCalendarImpl()
        {
            if (!_initialized)
            {
                _appointmentStore = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AppCalendarsReadWrite);
                throw new NotImplementedException();
                //if (BaseSettings.IsFirstRun)
                //{
                //    await CheckForAndCreateAppointmentCalendars();

                //    BaseSettings.IsFirstRun = false;
                //}
                //else
                //{
                //    await CheckForAndCreateAppointmentCalendars();
                //}
                //_initialized = true;
            }
        }

        private static async Task CheckForAndCreateAppointmentCalendars()
        {
            var appCalendars = await _appointmentStore.FindAppointmentCalendarsAsync(FindAppointmentCalendarsOptions.IncludeHidden);
            AppointmentCalendar appCalendar;

            if (appCalendars.Count == 0)
            {
                // TODO make generic!
                appCalendar = await _appointmentStore.CreateAppointmentCalendarAsync("CalendarName");
            }
            else
            {
                appCalendar = appCalendars[0];
            }

            appCalendar.OtherAppReadAccess = AppointmentCalendarOtherAppReadAccess.Full;
            appCalendar.OtherAppWriteAccess = AppointmentCalendarOtherAppWriteAccess.None;
            appCalendar.SummaryCardView = AppointmentSummaryCardView.System;

            await appCalendar.SaveAsync();

            _currentAppCalendar = appCalendar;
        }

        private static async Task<string> CreateAppointmentImpl(Appointment appointment)
        {
            await _currentAppCalendar.SaveAppointmentAsync(appointment);

            return appointment.LocalId;
        }

        private static async Task DeleteAppointmentImpl(string localId)
        {
            await _currentAppCalendar.DeleteAppointmentAsync(localId);
        }

        private static async Task ModifyAppointmentImpl(string localId)
        {
            var target = await _appointmentStore.GetAppointmentAsync(localId);

            // modify here

            var calendar = await _appointmentStore.GetAppointmentCalendarAsync(target.CalendarId);
            await calendar.SaveAppointmentAsync(target);
        }
    }
}
