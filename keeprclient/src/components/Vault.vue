<template>
  <div>
    <button type="button" class="btn" data-toggle="modal" data-target="#keepDetailsModal">
      <div class="row justify-content-between">
        {{ keep.name }}
        {{ keep.creator.picture }}
        {{ keep.img }}
      </div>
    </button>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
// import { keepsService } from '../services/KeepsService'
// import { logger } from '../utils/Logger'
// import Notification from '../utils/Notification'
export default {
  name: 'Item',
  props: {
    item: {
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
      state
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
