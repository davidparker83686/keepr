<template>
  <div>
    <button type="button" class="btn btn-info" data-toggle="modal" :data-target="'#keepDetailsModal'+ keep.id">
      <div class="row  justify-content-between">
        {{ keep.name }}
        <!-- {{ keep.creator.picture }}
        {{ keep.img }} -->
      </div>
    </button>
    <KeepDetailsModal :keep-prop="keep" />
  </div>
  <!-- @click="addView()" -->
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
// import { logger } from '../utils/Logger'
// import Notification from '../utils/Notification'
export default {
  name: 'Keep',
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      keep: computed(() => AppState.keeps)
    })
    return {
      state,
      async addView() {
        try {
          await keepsService.addView()
        } catch (error) {
          console.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
/* .dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}
.available {
  color: var(--success)
}
.not-available {
  color: var(--danger)
}
.lightgrey{
  background-color: rgba(236, 236, 236, 0.315);
}
@media screen and (min-width:760px){
  .userbuttons{
justify-content: space-around;
  }
} */
</style>
