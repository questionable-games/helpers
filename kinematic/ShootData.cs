//
// Copyright (C) 2019 Alberto Pérez García-Plaza
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//
// Authors:
//     Alberto Pérez García-Plaza <alpgarcia@gmail.com>
//

using UnityEngine;

public struct ShootData
{
    public readonly Vector3 initialVelocity;
    public readonly float timeToTarget;

    public ShootData(Vector3 initialVelocity, float timeToTarget)
    {
        this.initialVelocity = initialVelocity;
        this.timeToTarget = timeToTarget;
    }
}
