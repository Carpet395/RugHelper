// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// XaphanHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Celeste.Mod.XaphanHelper.Entities.CustomBadelineBoss
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Celeste;
using Celeste.Mod.Entities;
using Celeste.Mod.XaphanHelper.Entities;
using Microsoft.Xna.Framework;
using Monocle;

[Tracked(false)]
[CustomEntity(new string[] { "XaphanHelper/CustomBadelineBoss" })]
public class CustomBadelineBoss : Entity
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass50_0
	{
		public CustomBadelineBoss _003C_003E4__this;

		public float from2;

		public bool lastHit;

		public Vector2 from;

		public Vector2 to;

		public float dir;

		internal void _003CMoveSequence_003Eb__0(Tween t)
		{
			if (_003C_003E4__this.bossBg != null && _003C_003E4__this.bossBg.Alpha < t.Eased)
			{
				_003C_003E4__this.bossBg.Alpha = t.Eased;
			}
			Engine.TimeRate = MathHelper.Lerp(from2, 1f, t.Eased);
			if (lastHit)
			{
				Glitch.Value = 0.6f * (1f - t.Eased);
			}
		}

		internal void _003CMoveSequence_003Eb__1(Tween t)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			_003C_003E4__this.Position = Vector2.Lerp(from, to, t.Eased);
			if (t.Eased >= 0.1f && t.Eased <= 0.9f && _003C_003E4__this.Scene.OnInterval(0.02f))
			{
				TrailManager.Add(_003C_003E4__this, Calc.HexToColor(_003C_003E4__this.TrailColor), 0.5f, frozenUpdate: false, useRawDeltaTime: false);
				_003C_003E4__this.level.Particles.Emit(P_Dash, 2, _003C_003E4__this.Center, Vector2.One * 3f, dir);
			}
		}

		internal void _003CMoveSequence_003Eb__2(Tween _003Cp0_003E)
		{
			_003C_003E4__this.Sprite.Play("recoverHit");
			_003C_003E4__this.Moving = false;
			_003C_003E4__this.Collidable = true;
			Player entity = _003C_003E4__this.Scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				_003C_003E4__this.facing = Math.Sign(entity.X - _003C_003E4__this.X);
				if (_003C_003E4__this.facing == 0)
				{
					_003C_003E4__this.facing = -1;
				}
			}
			_003C_003E4__this.StartAttacking();
			_003C_003E4__this.floatSine.Reset();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack01Sequence_003Ed__57 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack01Sequence_003Ed__57(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E4__this.Shoot();
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.15f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E2__current = 0.5f;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack02Sequence_003Ed__58 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack02Sequence_003Ed__58(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003C_003E4__this.Beam();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E4__this.Shoot();
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E2__current = 0.5f;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack03Sequence_003Ed__59 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		private int _003Cj_003E5__1;

		private Player _003Centity_003E5__2;

		private Vector2 _003Cat_003E5__3;

		private int _003Ci_003E5__4;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack03Sequence_003Ed__59(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003Centity_003E5__2 = null;
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_01d1;
			case 2:
				_003C_003E1__state = -1;
				_003Ci_003E5__4++;
				goto IL_010f;
			case 3:
				_003C_003E1__state = -1;
				goto IL_015a;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					goto IL_01d1;
				}
				IL_01d1:
				_003Cj_003E5__1 = 0;
				goto IL_0172;
				IL_0172:
				if (_003Cj_003E5__1 < 5)
				{
					_003Centity_003E5__2 = _003C_003E4__this.level.Tracker.GetEntity<Player>();
					if (_003Centity_003E5__2 != null)
					{
						_003Cat_003E5__3 = _003Centity_003E5__2.Center;
						_003Ci_003E5__4 = 0;
						goto IL_010f;
					}
					goto IL_011d;
				}
				_003C_003E2__current = 2f;
				_003C_003E1__state = 4;
				return true;
				IL_011d:
				if (_003Cj_003E5__1 < 4)
				{
					_003C_003E4__this.StartShootCharge();
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_015a;
				IL_015a:
				_003Centity_003E5__2 = null;
				_003Cj_003E5__1++;
				goto IL_0172;
				IL_010f:
				if (_003Ci_003E5__4 < 2)
				{
					_003C_003E4__this.ShootAt(_003Cat_003E5__3);
					_003C_003E2__current = 0.15f;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_011d;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack04Sequence_003Ed__60 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		private int _003Cj_003E5__1;

		private Player _003Centity_003E5__2;

		private Vector2 _003Cat_003E5__3;

		private int _003Ci_003E5__4;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack04Sequence_003Ed__60(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003Centity_003E5__2 = null;
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_01fb;
			case 2:
				_003C_003E1__state = -1;
				_003Ci_003E5__4++;
				goto IL_0118;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0163;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003C_003E4__this.Beam();
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					_003C_003E4__this.StartShootCharge();
					goto IL_01fb;
				}
				IL_01fb:
				_003Cj_003E5__1 = 0;
				goto IL_017b;
				IL_017b:
				if (_003Cj_003E5__1 < 5)
				{
					_003Centity_003E5__2 = _003C_003E4__this.level.Tracker.GetEntity<Player>();
					if (_003Centity_003E5__2 != null)
					{
						_003Cat_003E5__3 = _003Centity_003E5__2.Center;
						_003Ci_003E5__4 = 0;
						goto IL_0118;
					}
					goto IL_0126;
				}
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 4;
				return true;
				IL_0126:
				if (_003Cj_003E5__1 < 4)
				{
					_003C_003E4__this.StartShootCharge();
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_0163;
				IL_0163:
				_003Centity_003E5__2 = null;
				_003Cj_003E5__1++;
				goto IL_017b;
				IL_0118:
				if (_003Ci_003E5__4 < 2)
				{
					_003C_003E4__this.ShootAt(_003Cat_003E5__3);
					_003C_003E2__current = 0.15f;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_0126;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack05Sequence_003Ed__61 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		private int _003Cj_003E5__1;

		private Player _003Centity_003E5__2;

		private Vector2 _003Cat_003E5__3;

		private int _003Ci_003E5__4;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack05Sequence_003Ed__61(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003Centity_003E5__2 = null;
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				break;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003Cj_003E5__1 = 0;
				goto IL_01df;
			case 5:
				_003C_003E1__state = -1;
				_003Ci_003E5__4++;
				goto IL_017c;
			case 6:
				_003C_003E1__state = -1;
				goto IL_01c7;
			case 7:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_01df:
				if (_003Cj_003E5__1 < 3)
				{
					_003Centity_003E5__2 = _003C_003E4__this.level.Tracker.GetEntity<Player>();
					if (_003Centity_003E5__2 != null)
					{
						_003Cat_003E5__3 = _003Centity_003E5__2.Center;
						_003Ci_003E5__4 = 0;
						goto IL_017c;
					}
					goto IL_018a;
				}
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 7;
				return true;
				IL_017c:
				if (_003Ci_003E5__4 < 2)
				{
					_003C_003E4__this.ShootAt(_003Cat_003E5__3);
					_003C_003E2__current = 0.15f;
					_003C_003E1__state = 5;
					return true;
				}
				goto IL_018a;
				IL_018a:
				if (_003Cj_003E5__1 < 2)
				{
					_003C_003E4__this.StartShootCharge();
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 6;
					return true;
				}
				goto IL_01c7;
				IL_01c7:
				_003Centity_003E5__2 = null;
				_003Cj_003E5__1++;
				goto IL_01df;
			}
			_003C_003E2__current = _003C_003E4__this.Beam();
			_003C_003E1__state = 2;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack06Sequence_003Ed__62 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack06Sequence_003Ed__62(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E2__current = _003C_003E4__this.Beam();
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack07Sequence_003Ed__63 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack07Sequence_003Ed__63(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E4__this.Shoot();
			_003C_003E2__current = 0.8f;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack08Sequence_003Ed__64 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack08Sequence_003Ed__64(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003C_003E4__this.Beam();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E2__current = 0.1f;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack09Sequence_003Ed__65 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack09Sequence_003Ed__65(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E4__this.Shoot();
				_003C_003E2__current = 0.15f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E4__this.Shoot();
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E2__current = 0.5f;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack10Sequence_003Ed__66 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack10Sequence_003Ed__66(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			if (_003C_003E1__state != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack11Sequence_003Ed__67 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack11Sequence_003Ed__67(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (_003C_003E4__this.nodeIndex == 0)
				{
					_003C_003E4__this.StartShootCharge();
					_003C_003E2__current = 0.6f;
					_003C_003E1__state = 1;
					return true;
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E4__this.StartShootCharge();
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E4__this.Shoot();
			_003C_003E2__current = 1.9f;
			_003C_003E1__state = 2;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack13Sequence_003Ed__68 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack13Sequence_003Ed__68(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (_003C_003E4__this.nodeIndex != 0)
				{
					_003C_003E2__current = _003C_003E4__this.Attack01Sequence();
					_003C_003E1__state = 1;
					return true;
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack14Sequence_003Ed__69 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack14Sequence_003Ed__69(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003C_003E4__this.Beam();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E2__current = 0.2f;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAttack15Sequence_003Ed__70 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CAttack15Sequence_003Ed__70(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003C_003E4__this.Beam();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				break;
			}
			_003C_003E2__current = 0.2f;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CBeam_003Ed__73 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CustomBadelineBoss _003C_003E4__this;

		private Player _003Centity_003E5__1;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CBeam_003Ed__73(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003Centity_003E5__1 = null;
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E4__this.laserSfx.Play("event:/char/badeline/boss_laser_charge");
				_003C_003E4__this.Sprite.Play("attack2Begin", restart: true);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Centity_003E5__1 = _003C_003E4__this.level.Tracker.GetEntity<Player>();
				if (_003Centity_003E5__1 != null)
				{
					_003C_003E4__this.level.Add(Engine.Pooler.Create<CustomBadelineBossBeam>().Init(_003C_003E4__this, _003Centity_003E5__1, _003C_003E4__this.BeamDissipateParticleColor));
				}
				_003C_003E2__current = 0.9f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E4__this.Sprite.Play("attack2Lock", restart: true);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E4__this.laserSfx.Stop();
				Audio.Play("event:/char/badeline/boss_laser_fire", _003C_003E4__this.Position);
				_003C_003E4__this.Sprite.Play("attack2Recoil");
				return false;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CMoveSequence_003Ed__50 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Player player;

		public bool lastHit;

		public CustomBadelineBoss _003C_003E4__this;

		private _003C_003Ec__DisplayClass50_0 _003C_003E8__1;

		private float _003Ctimer_003E5__2;

		private Tween _003Ctween3_003E5__3;

		private float _003Cduration_003E5__4;

		private Tween _003Ctween4_003E5__5;

		private Tween _003Ctween_003E5__6;

		private Tween _003Ctween2_003E5__7;

		private List<Entity>.Enumerator _003C_003Es__8;

		private ReflectionTentacles _003Centity2_003E5__9;

		private float _003Cnum_003E5__10;

		private Vector2 _003Cposition_003E5__11;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CMoveSequence_003Ed__50(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E8__1 = null;
			_003Ctween3_003E5__3 = null;
			_003Ctween4_003E5__5 = null;
			_003Ctween_003E5__6 = null;
			_003Ctween2_003E5__7 = null;
			_003C_003Es__8 = default(List<Entity>.Enumerator);
			_003Centity2_003E5__9 = null;
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04be: Unknown result type (might be due to invalid IL or missing references)
			//IL_04df: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_051c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0527: Unknown result type (might be due to invalid IL or missing references)
			//IL_052c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0357: Unknown result type (might be due to invalid IL or missing references)
			//IL_0367: Unknown result type (might be due to invalid IL or missing references)
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_039b: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_0179: Unknown result type (might be due to invalid IL or missing references)
			//IL_0183: Unknown result type (might be due to invalid IL or missing references)
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass50_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E8__1.lastHit = lastHit;
				if (_003C_003E8__1.lastHit)
				{
					Audio.SetMusicParam("boss_pitch", 1f);
					_003Ctween_003E5__6 = Tween.Create(Tween.TweenMode.Oneshot, null, 0.3f, start: true);
					_003Ctween_003E5__6.OnUpdate = delegate(Tween t)
					{
						Glitch.Value = 0.6f * t.Eased;
					};
					_003C_003E4__this.Add(_003Ctween_003E5__6);
					_003Ctween_003E5__6 = null;
				}
				else
				{
					_003Ctween2_003E5__7 = Tween.Create(Tween.TweenMode.Oneshot, null, 0.3f, start: true);
					_003Ctween2_003E5__7.OnUpdate = delegate(Tween t)
					{
						Glitch.Value = 0.5f * (1f - t.Eased);
					};
					_003C_003E4__this.Add(_003Ctween2_003E5__7);
					_003Ctween2_003E5__7 = null;
				}
				if (player != null && !player.Dead)
				{
					player.StartAttract(_003C_003E4__this.Center + Vector2.UnitY * 4f);
				}
				_003Ctimer_003E5__2 = 0.15f;
				goto IL_01cc;
			case 1:
				_003C_003E1__state = -1;
				_003Ctimer_003E5__2 -= Engine.DeltaTime;
				goto IL_01cc;
			case 2:
				_003C_003E1__state = -1;
				goto IL_022c;
			case 3:
				_003C_003E1__state = -1;
				for (_003Cnum_003E5__10 = 0f; _003Cnum_003E5__10 < (float)Math.PI * 2f; _003Cnum_003E5__10 += 0.17453292f)
				{
					_003Cposition_003E5__11 = _003C_003E4__this.Center + _003C_003E4__this.Sprite.Position + Calc.AngleToVector(_003Cnum_003E5__10 + Calc.Random.Range(-(float)Math.PI / 90f, (float)Math.PI / 90f), Calc.Random.Range(16, 20));
					_003C_003E4__this.level.Particles.Emit(P_Burst, _003Cposition_003E5__11, _003Cnum_003E5__10);
				}
				_003C_003E2__current = 0.05f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				Audio.SetMusicParam("boss_pitch", 0f);
				_003C_003E8__1.from2 = Engine.TimeRate;
				_003Ctween3_003E5__3 = Tween.Create(Tween.TweenMode.Oneshot, null, 0.35f / Engine.TimeRateB, start: true);
				_003Ctween3_003E5__3.UseRawDeltaTime = true;
				_003Ctween3_003E5__3.OnUpdate = delegate(Tween t)
				{
					if (_003C_003E8__1._003C_003E4__this.bossBg != null && _003C_003E8__1._003C_003E4__this.bossBg.Alpha < t.Eased)
					{
						_003C_003E8__1._003C_003E4__this.bossBg.Alpha = t.Eased;
					}
					Engine.TimeRate = MathHelper.Lerp(_003C_003E8__1.from2, 1f, t.Eased);
					if (_003C_003E8__1.lastHit)
					{
						Glitch.Value = 0.6f * (1f - t.Eased);
					}
				};
				_003C_003E4__this.Add(_003Ctween3_003E5__3);
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					_003C_003E8__1.from = _003C_003E4__this.Position;
					_003C_003E8__1.to = _003C_003E4__this.nodes[_003C_003E4__this.nodeIndex];
					_003Cduration_003E5__4 = Vector2.Distance(_003C_003E8__1.from, _003C_003E8__1.to) / 600f;
					_003C_003E8__1.dir = (_003C_003E8__1.to - _003C_003E8__1.from).Angle();
					_003Ctween4_003E5__5 = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, _003Cduration_003E5__4, start: true);
					_003Ctween4_003E5__5.OnUpdate = delegate(Tween t)
					{
						//IL_0008: Unknown result type (might be due to invalid IL or missing references)
						//IL_000e: Unknown result type (might be due to invalid IL or missing references)
						//IL_0019: Unknown result type (might be due to invalid IL or missing references)
						//IL_001e: Unknown result type (might be due to invalid IL or missing references)
						//IL_006b: Unknown result type (might be due to invalid IL or missing references)
						//IL_0099: Unknown result type (might be due to invalid IL or missing references)
						//IL_009e: Unknown result type (might be due to invalid IL or missing references)
						//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
						_003C_003E8__1._003C_003E4__this.Position = Vector2.Lerp(_003C_003E8__1.from, _003C_003E8__1.to, t.Eased);
						if (t.Eased >= 0.1f && t.Eased <= 0.9f && _003C_003E8__1._003C_003E4__this.Scene.OnInterval(0.02f))
						{
							TrailManager.Add(_003C_003E8__1._003C_003E4__this, Calc.HexToColor(_003C_003E8__1._003C_003E4__this.TrailColor), 0.5f, frozenUpdate: false, useRawDeltaTime: false);
							_003C_003E8__1._003C_003E4__this.level.Particles.Emit(P_Dash, 2, _003C_003E8__1._003C_003E4__this.Center, Vector2.One * 3f, _003C_003E8__1.dir);
						}
					};
					_003Ctween4_003E5__5.OnComplete = delegate
					{
						_003C_003E8__1._003C_003E4__this.Sprite.Play("recoverHit");
						_003C_003E8__1._003C_003E4__this.Moving = false;
						_003C_003E8__1._003C_003E4__this.Collidable = true;
						Player entity = _003C_003E8__1._003C_003E4__this.Scene.Tracker.GetEntity<Player>();
						if (entity != null)
						{
							_003C_003E8__1._003C_003E4__this.facing = Math.Sign(entity.X - _003C_003E8__1._003C_003E4__this.X);
							if (_003C_003E8__1._003C_003E4__this.facing == 0)
							{
								_003C_003E8__1._003C_003E4__this.facing = -1;
							}
						}
						_003C_003E8__1._003C_003E4__this.StartAttacking();
						_003C_003E8__1._003C_003E4__this.floatSine.Reset();
					};
					_003C_003E4__this.Add(_003Ctween4_003E5__5);
					return false;
				}
				IL_022c:
				_003C_003Es__8 = _003C_003E4__this.Scene.Tracker.GetEntities<ReflectionTentacles>().GetEnumerator();
				try
				{
					while (_003C_003Es__8.MoveNext())
					{
						_003Centity2_003E5__9 = (ReflectionTentacles)_003C_003Es__8.Current;
						_003Centity2_003E5__9.Retreat();
						_003Centity2_003E5__9 = null;
					}
				}
				finally
				{
					((IDisposable)_003C_003Es__8).Dispose();
				}
				_003C_003Es__8 = default(List<Entity>.Enumerator);
				if (player != null)
				{
					global::Celeste.Celeste.Freeze(0.1f);
					if (_003C_003E8__1.lastHit)
					{
						Engine.TimeRate = 0.5f;
					}
					else
					{
						Engine.TimeRate = 0.75f;
					}
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				}
				_003C_003E4__this.PushPlayer(player);
				_003C_003E4__this.level.Shake();
				_003C_003E2__current = 0.05f;
				_003C_003E1__state = 3;
				return true;
				IL_01cc:
				if (player != null && !player.Dead && !player.AtAttractTarget)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (_003Ctimer_003E5__2 > 0f)
				{
					_003C_003E2__current = _003Ctimer_003E5__2;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_022c;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static ParticleType P_Burst = new ParticleType();

	public static ParticleType P_Dash = new ParticleType();

	public bool cameraLockY;

	public const float CameraXPastMax = 140f;

	private const float MoveSpeed = 600f;

	private const float AvoidRadius = 12f;

	public Sprite Sprite;

	public PlayerSprite NormalSprite;

	private PlayerHair normalHair;

	private Vector2 avoidPos;

	public float CameraYPastMax;

	public bool Moving;

	public bool Sitting;

	private int facing;

	private Level level;

	private Circle circle;

	private Vector2[] nodes;

	private int nodeIndex;

	private int patternIndex;

	private Coroutine attackCoroutine;

	private Coroutine triggerBlocksCoroutine;

	private List<Entity> fallingBlocks;

	private List<Entity> movingBlocks;

	private bool playerHasMoved;

	private SineWave floatSine;

	private bool startHit;

	private VertexLight light;

	private Wiggler scaleWiggler;

	private FinalBossStarfield bossBg;

	private SoundSource chargeSfx;

	private SoundSource laserSfx;

	private bool canChangeMusic;

	public string ShotTrailParticleColor1;

	public string ShotTrailParticleColor2;

	public string BeamDissipateParticleColor;

	public string TrailColor;

	private string spriteName;

	private bool cameraLock;

	private bool drawProjectilesOutline;

	public Vector2 BeamOrigin => base.Center + Sprite.Position + new Vector2(0f, -14f);

	public Vector2 ShotOrigin => base.Center + Sprite.Position + new Vector2(6f * Sprite.Scale.X, 2f);

	public CustomBadelineBoss(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		canChangeMusic = data.Bool("canChangeMusic", defaultValue: true);
		patternIndex = data.Int("patternIndex");
		CameraYPastMax = data.Float("cameraYPastMax");
		startHit = data.Bool("startHit");
		cameraLock = data.Bool("cameraLock", defaultValue: true);
		cameraLockY = data.Bool("cameraLockY");
		spriteName = data.Attr("spriteName");
		if (string.IsNullOrEmpty(spriteName))
		{
			spriteName = "badeline_boss";
		}
		ShotTrailParticleColor1 = data.Attr("shotTrailParticleColor1", "ffced5");
		ShotTrailParticleColor2 = data.Attr("shotTrailParticleColor2", "ff4f7d");
		BeamDissipateParticleColor = data.Attr("beamDissipateParticleColor", "e60022");
		TrailColor = data.Attr("trailColor", "ac3232");
		Add(light = new VertexLight(Color.White, 1f, 32, 64));
		base.Collider = (circle = new Circle(14f, 0f, -6f));
		Add(new PlayerCollider(OnPlayer));
		nodes = (Vector2[])(object)new Vector2[data.Nodes.GetLength(0) + 1];
		nodes[0] = data.Position + offset;
		for (int i = 0; i < data.Nodes.GetLength(0); i++)
		{
			nodes[i + 1] = data.Nodes[i] + offset;
		}
		attackCoroutine = new Coroutine(removeOnComplete: false);
		Add(attackCoroutine);
		triggerBlocksCoroutine = new Coroutine(removeOnComplete: false);
		Add(triggerBlocksCoroutine);
		if (cameraLock)
		{
			Add(new CameraLocker(cameraLockY ? Level.CameraLockModes.FinalBoss : Level.CameraLockModes.FinalBossNoY, 140f, CameraYPastMax));
		}
		Add(floatSine = new SineWave(0.6f, 0f));
		Add(scaleWiggler = Wiggler.Create(0.6f, 3f));
		Add(chargeSfx = new SoundSource());
		Add(laserSfx = new SoundSource());
		P_Burst = new ParticleType
		{
			Color = Calc.HexToColor(data.Attr("hitParticleColor1", "ff00b0")),
			Color2 = Calc.HexToColor(data.Attr("hitParticleColor2", "ff84d9")),
			ColorMode = ParticleType.ColorModes.Blink,
			FadeMode = ParticleType.FadeModes.Late,
			Size = 1f,
			DirectionRange = (float)Math.PI / 3f,
			SpeedMin = 40f,
			SpeedMax = 100f,
			SpeedMultiplier = 0.2f,
			LifeMin = 0.4f,
			LifeMax = 0.8f
		};
		P_Dash = new ParticleType
		{
			Color = Calc.HexToColor(data.Attr("MoveParticleColor1", "AC3232")),
			Color2 = Calc.HexToColor(data.Attr("MoveParticleColor2", "e05959")),
			ColorMode = ParticleType.ColorModes.Blink,
			FadeMode = ParticleType.FadeModes.Late,
			LifeMin = 1f,
			LifeMax = 1.8f,
			Size = 1f,
			SpeedMin = 10f,
			SpeedMax = 20f,
			Acceleration = new Vector2(0f, 8f),
			DirectionRange = (float)Math.PI / 3f
		};
		drawProjectilesOutline = data.Bool("drawProjectilesOutline", defaultValue: true);
	}

	public override void Added(Scene scene)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		base.Added(scene);
		level = SceneAs<Level>();
		if (patternIndex == 0)
		{
			NormalSprite = new PlayerSprite(PlayerSpriteMode.Badeline);
			NormalSprite.Scale.X = -1f;
			NormalSprite.Play("laugh");
			normalHair = new PlayerHair(NormalSprite);
			normalHair.Color = BadelineOldsite.HairColor;
			normalHair.Border = Color.Black;
			normalHair.Facing = Facings.Left;
			Add(normalHair);
			Add(NormalSprite);
		}
		else
		{
			CreateBossSprite();
		}
		bossBg = level.Background.Get<FinalBossStarfield>();
		if (patternIndex == 0 && !level.Session.GetFlag("boss_intro") && level.Session.Level.Equals("boss-00"))
		{
			level.Session.Audio.Music.Event = "event:/music/lvl2/phone_loop";
			level.Session.Audio.Apply(forceSixteenthNoteHack: false);
			if (bossBg != null)
			{
				bossBg.Alpha = 0f;
			}
			Sitting = true;
			Position.Y += 16f;
			NormalSprite.Play("pretendDead");
			NormalSprite.Scale.X = 1f;
		}
		else if (patternIndex == 0 && !level.Session.GetFlag("boss_mid") && level.Session.Level.Equals("boss-14"))
		{
			level.Add(new CS06_BossMid());
		}
		else if (startHit)
		{
			Alarm.Set(this, 0.5f, delegate
			{
				OnPlayer(null);
			});
		}
		light.Position = ((Sprite != null) ? Sprite : NormalSprite).Position + new Vector2(0f, -10f);
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		fallingBlocks = base.Scene.Tracker.GetEntitiesCopy<FallingBlock>();
		fallingBlocks.Sort((Entity a, Entity b) => (int)(a.X - b.X));
		movingBlocks = base.Scene.Tracker.GetEntitiesCopy<FinalBossMovingBlock>();
		movingBlocks.Sort((Entity a, Entity b) => (int)(a.X - b.X));
	}

	private void CreateBossSprite()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Add(Sprite = GFX.SpriteBank.Create(spriteName));
		Sprite.OnFrameChange = delegate(string anim)
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			if (anim == "idle" && Sprite.CurrentAnimationFrame == 18)
			{
				Audio.Play("event:/char/badeline/boss_idle_air", Position);
			}
		};
		facing = -1;
		if (NormalSprite != null)
		{
			Sprite.Position = NormalSprite.Position;
			Remove(NormalSprite);
		}
		if (normalHair != null)
		{
			Remove(normalHair);
		}
		NormalSprite = null;
		normalHair = null;
	}

	public override void Update()
	{
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		Sprite sprite = ((Sprite != null) ? Sprite : NormalSprite);
		if (!Sitting)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (!Moving && entity != null)
			{
				if (facing == -1 && entity.X > base.X + 20f)
				{
					facing = 1;
					scaleWiggler.Start();
				}
				else if (facing == 1 && entity.X < base.X - 20f)
				{
					facing = -1;
					scaleWiggler.Start();
				}
			}
			if (!playerHasMoved && entity != null && entity.Speed != Vector2.Zero)
			{
				playerHasMoved = true;
				if (patternIndex != 0)
				{
					StartAttacking();
				}
				TriggerMovingBlocks(0);
			}
			if (!Moving)
			{
				sprite.Position = avoidPos + new Vector2(floatSine.Value * 3f, floatSine.ValueOverTwo * 4f);
			}
			else
			{
				sprite.Position = Calc.Approach(sprite.Position, Vector2.Zero, 12f * Engine.DeltaTime);
			}
			float radius = circle.Radius;
			circle.Radius = 6f;
			CollideFirst<DashBlock>()?.Break(base.Center, -Vector2.UnitY, playSound: true);
			circle.Radius = radius;
			if (!level.IsInBounds(Position, 24f))
			{
				Active = (Visible = (Collidable = false));
				return;
			}
			Vector2 target;
			if (!Moving && entity != null)
			{
				Vector2 val = base.Center - entity.Center;
				float val2 = ((Vector2)(ref val)).Length();
				val2 = Calc.ClampedMap(val2, 32f, 88f, 12f, 0f);
				target = ((!(val2 <= 0f)) ? (base.Center - entity.Center).SafeNormalize(val2) : Vector2.Zero);
			}
			else
			{
				target = Vector2.Zero;
			}
			avoidPos = Calc.Approach(avoidPos, target, 40f * Engine.DeltaTime);
		}
		light.Position = sprite.Position + new Vector2(0f, -10f);
	}

	public override void Render()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (Sprite != null)
		{
			Sprite.Scale.X = facing;
			Sprite.Scale.Y = 1f;
			Sprite sprite = Sprite;
			sprite.Scale *= 1f + scaleWiggler.Value * 0.2f;
		}
		if (NormalSprite != null)
		{
			Vector2 position = NormalSprite.Position;
			NormalSprite.Position = NormalSprite.Position.Floor();
			base.Render();
			NormalSprite.Position = position;
		}
		else
		{
			base.Render();
		}
	}

	public void OnPlayer(Player player)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (Sprite == null)
		{
			CreateBossSprite();
		}
		Sprite.Play("getHit");
		Audio.Play("event:/char/badeline/boss_hug", Position);
		chargeSfx.Stop();
		if (laserSfx.EventName == "event:/char/badeline/boss_laser_charge" && laserSfx.Playing)
		{
			laserSfx.Stop();
		}
		Collidable = false;
		avoidPos = Vector2.Zero;
		nodeIndex++;
		foreach (CustomBadelineBossShot entity in level.Tracker.GetEntities<CustomBadelineBossShot>())
		{
			entity.Destroy();
		}
		foreach (CustomBadelineBossBeam entity2 in level.Tracker.GetEntities<CustomBadelineBossBeam>())
		{
			entity2.Destroy();
		}
		TriggerFallingBlocks(base.X);
		TriggerMovingBlocks(nodeIndex);
		attackCoroutine.Active = false;
		Moving = true;
		bool flag = nodeIndex == nodes.Length - 1;
		if (CanChangeMusic(level.Session.Area.Mode == AreaMode.Normal))
		{
			if (flag && level.Session.Level.Equals("boss-19"))
			{
				Alarm.Set(this, 0.25f, delegate
				{
					Audio.Play("event:/game/06_reflection/boss_spikes_burst");
					foreach (CrystalStaticSpinner entity3 in base.Scene.Tracker.GetEntities<CrystalStaticSpinner>())
					{
						entity3.Destroy(boss: true);
					}
				});
				Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "postboss", 1f);
				Audio.SetMusic(null);
			}
			else if (startHit && level.Session.Audio.Music.Event != "event:/music/lvl6/badeline_glitch")
			{
				level.Session.Audio.Music.Event = "event:/music/lvl6/badeline_glitch";
				level.Session.Audio.Apply(forceSixteenthNoteHack: false);
			}
			else if (level.Session.Audio.Music.Event != "event:/music/lvl6/badeline_fight" && level.Session.Audio.Music.Event != "event:/music/lvl6/badeline_glitch")
			{
				level.Session.Audio.Music.Event = "event:/music/lvl6/badeline_fight";
				level.Session.Audio.Apply(forceSixteenthNoteHack: false);
			}
		}
		Add(new Coroutine(MoveSequence(player, flag)));
	}

	[IteratorStateMachine(typeof(_003CMoveSequence_003Ed__50))]
	private IEnumerator MoveSequence(Player player, bool lastHit)
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CMoveSequence_003Ed__50(0)
		{
			_003C_003E4__this = this,
			player = player,
			lastHit = lastHit
		};
	}

	private void PushPlayer(Player player)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		if (player != null && !player.Dead)
		{
			int num = Math.Sign(base.X - nodes[nodeIndex].X);
			if (num == 0)
			{
				num = -1;
			}
			player.FinalBossPushLaunch(num);
		}
		SceneAs<Level>().Displacement.AddBurst(Position, 0.4f, 12f, 36f, 0.5f);
		SceneAs<Level>().Displacement.AddBurst(Position, 0.4f, 24f, 48f, 0.5f);
		SceneAs<Level>().Displacement.AddBurst(Position, 0.4f, 36f, 60f, 0.5f);
	}

	private void TriggerFallingBlocks(float leftOfX)
	{
		while (fallingBlocks.Count > 0 && fallingBlocks[0].Scene == null)
		{
			fallingBlocks.RemoveAt(0);
		}
		int num = 0;
		while (fallingBlocks.Count > 0 && fallingBlocks[0].X < leftOfX)
		{
			FallingBlock fallingBlock = fallingBlocks[0] as FallingBlock;
			fallingBlock.StartShaking();
			fallingBlock.Triggered = true;
			fallingBlock.FallDelay = 0.4f * (float)num;
			num++;
			fallingBlocks.RemoveAt(0);
		}
	}

	private void TriggerMovingBlocks(int nodeIndex)
	{
		if (nodeIndex > 0)
		{
			DestroyMovingBlocks(nodeIndex - 1);
		}
		float num = 0f;
		foreach (FinalBossMovingBlock movingBlock in movingBlocks)
		{
			if (movingBlock.BossNodeIndex == nodeIndex)
			{
				movingBlock.StartMoving(num);
				num += 0.15f;
			}
		}
	}

	private void DestroyMovingBlocks(int nodeIndex)
	{
		float num = 0f;
		foreach (FinalBossMovingBlock movingBlock in movingBlocks)
		{
			if (movingBlock.BossNodeIndex == nodeIndex)
			{
				movingBlock.Destroy(num);
				num += 0.05f;
			}
		}
	}

	private void StartAttacking()
	{
		switch (patternIndex)
		{
		case 12:
			break;
		case 0:
		case 1:
			attackCoroutine.Replace(Attack01Sequence());
			break;
		case 2:
			attackCoroutine.Replace(Attack02Sequence());
			break;
		case 3:
			attackCoroutine.Replace(Attack03Sequence());
			break;
		case 4:
			attackCoroutine.Replace(Attack04Sequence());
			break;
		case 5:
			attackCoroutine.Replace(Attack05Sequence());
			break;
		case 6:
			attackCoroutine.Replace(Attack06Sequence());
			break;
		case 7:
			attackCoroutine.Replace(Attack07Sequence());
			break;
		case 8:
			attackCoroutine.Replace(Attack08Sequence());
			break;
		case 9:
			attackCoroutine.Replace(Attack09Sequence());
			break;
		case 10:
			attackCoroutine.Replace(Attack10Sequence());
			break;
		case 11:
			attackCoroutine.Replace(Attack11Sequence());
			break;
		case 13:
			attackCoroutine.Replace(Attack13Sequence());
			break;
		case 14:
			attackCoroutine.Replace(Attack14Sequence());
			break;
		case 15:
			attackCoroutine.Replace(Attack15Sequence());
			break;
		}
	}

	private void StartShootCharge()
	{
		Sprite.Play("attack1Begin");
		chargeSfx.Play("event:/char/badeline/boss_bullet");
	}

	[IteratorStateMachine(typeof(_003CAttack01Sequence_003Ed__57))]
	private IEnumerator Attack01Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack01Sequence_003Ed__57(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack02Sequence_003Ed__58))]
	private IEnumerator Attack02Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack02Sequence_003Ed__58(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack03Sequence_003Ed__59))]
	private IEnumerator Attack03Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack03Sequence_003Ed__59(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack04Sequence_003Ed__60))]
	private IEnumerator Attack04Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack04Sequence_003Ed__60(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack05Sequence_003Ed__61))]
	private IEnumerator Attack05Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack05Sequence_003Ed__61(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack06Sequence_003Ed__62))]
	private IEnumerator Attack06Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack06Sequence_003Ed__62(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack07Sequence_003Ed__63))]
	private IEnumerator Attack07Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack07Sequence_003Ed__63(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack08Sequence_003Ed__64))]
	private IEnumerator Attack08Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack08Sequence_003Ed__64(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack09Sequence_003Ed__65))]
	private IEnumerator Attack09Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack09Sequence_003Ed__65(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack10Sequence_003Ed__66))]
	private IEnumerator Attack10Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack10Sequence_003Ed__66(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack11Sequence_003Ed__67))]
	private IEnumerator Attack11Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack11Sequence_003Ed__67(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack13Sequence_003Ed__68))]
	private IEnumerator Attack13Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack13Sequence_003Ed__68(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack14Sequence_003Ed__69))]
	private IEnumerator Attack14Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack14Sequence_003Ed__69(0)
		{
			_003C_003E4__this = this
		};
	}

	[IteratorStateMachine(typeof(_003CAttack15Sequence_003Ed__70))]
	private IEnumerator Attack15Sequence()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CAttack15Sequence_003Ed__70(0)
		{
			_003C_003E4__this = this
		};
	}

	private void Shoot(float angleOffset = 0f)
	{
		if (!chargeSfx.Playing)
		{
			chargeSfx.Play("event:/char/badeline/boss_bullet", "end", 1f);
		}
		else
		{
			chargeSfx.Param("end", 1f);
		}
		Sprite.Play("attack1Recoil", restart: true);
		Player entity = level.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			level.Add(Engine.Pooler.Create<CustomBadelineBossShot>().Init(this, entity, ShotTrailParticleColor1, ShotTrailParticleColor2, angleOffset, drawProjectilesOutline));
		}
	}

	private void ShootAt(Vector2 at)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (!chargeSfx.Playing)
		{
			chargeSfx.Play("event:/char/badeline/boss_bullet", "end", 1f);
		}
		else
		{
			chargeSfx.Param("end", 1f);
		}
		Sprite.Play("attack1Recoil", restart: true);
		level.Add(Engine.Pooler.Create<CustomBadelineBossShot>().InitAt(this, at, ShotTrailParticleColor1, ShotTrailParticleColor2, drawProjectilesOutline));
	}

	[IteratorStateMachine(typeof(_003CBeam_003Ed__73))]
	private IEnumerator Beam()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CBeam_003Ed__73(0)
		{
			_003C_003E4__this = this
		};
	}

	public override void Removed(Scene scene)
	{
		if (bossBg != null && patternIndex == 0)
		{
			bossBg.Alpha = 1f;
		}
		base.Removed(scene);
	}

	public bool CanChangeMusic(bool value)
	{
		if ((base.Scene as Level).Session.Area.LevelSet == "Celeste")
		{
			return value;
		}
		return canChangeMusic;
	}
}
