<template>
  <div class="position-relative m-3" style="min-width:20vw;">
    <button type="button card" class="btn btn-none shadow-none" data-toggle="modal" data-target="#copyKeepDetailsModal">
      <div class="row  justify-content-between" @click="activeKeep(keep)">
        <div class="col-10 d-flex justify-content-between" v-if="keep.creator">
          <h4 class="position-absolute m-2">
            {{ keep.name }}
          </h4>
          <img :src="keep.creator.picture " class="m-2 rounded-circle small-img position-absolute" alt="">
        </div>
        <img :src="keep.img" class="img-fluid " style="min-width:100px;" alt="">
      </div>
    </button>

    <CopyKeepDetailsModal />
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
      async activeKeep(keep) {
        try {
          await keepsService.activeKeep(keep)
        } catch (error) {
          console.error(error)
        }
      },
      async addView() {
        try {
          await keepsService.addView()
        } catch (error) {
          console.error(error)
        }
      }
      // async getUserRating() {
      //   const totalReviews = keep.img
      //   let sumOfReviews = 0
      //   state.reviews.forEach(r => {
      //     sumOfReviews += r.rating
      //   })
      //   const userRating = sumOfReviews / totalReviews
      //   return Math.round(userRating)
      //   // return userRating
      // }
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
/* .grid-image{

} */
 img{
   width: 100%
 }
 .small-img{
   width: 1em;
   left: 0;
   top: 5px}
</style>
