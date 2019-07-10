using CycleTogether.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace CycleTogether.RoutesSubscriber
{
    public static class Requirements
    {
        public static bool Match(UserEntry user, RouteEntry route)
        {
            //user.Terrain, user.Difficulty, user.Endurance,
            if (EquipmentMatch(user.UserEquipments, route.RouteEquipments) &&
                user.Endurance >= route.Endurance &&
                user.Terrain >= route.Terrain &&
                user.Difficulty >= route.Difficulty)
            {
                return true;
            }

            return false;
        }

        private static bool EquipmentMatch(IList<UserEquipmentEntry> userEquipments, IList<RouteEquipmentEntry> routeEquipments)
        {
            foreach (var equipment in routeEquipments)
            {
                foreach (var userEquipment in userEquipments)
                {
                    if (equipment.EquipmentId != userEquipment.EquipmentId)
                    {
                        return false;
                    }
                }              
            }
            return true;
        }
    }
}
