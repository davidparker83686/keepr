<template>
  <div class="position-relative my-3" type="button card" data-toggle="modal" data-target="#copyKeepDetailsModal" @click="activeKeep(keep)">
    <button v-if=" keep.vaultKeepId "
            type="button"
            class="btn btn-outline-none shadow-none  position-absolute  mt-2 text-danger"
            title="Delete Keep"
            aria-label="Delete Keep"
            @click="deleteVaultKeep(keep.vaultKeepId)"
            data-dismiss="modal"
    >
      <i class="fas fa-trash-alt"></i>
    </button>
    <div class="position-absolute w-100 d-flex justify-content-between bottom">
      <h4 class="m-2 text backg ">
        <b>
          {{ keep.name.toUpperCase() }}
        </b>
      </h4>
      <div>
        <img :src="keep.creator.picture " class="m-2 glow rounded-circle small-img" alt="" v-if="keep.creator">
      </div>
    </div>

    <img :src="keep.img" class="img-fluid rounded" style="min-width:100px;" alt="" v-if="keep">
    <CopyKeepDetailsModal />
  <!-- @click="addView()" -->
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
// import { logger } from '../utils/Logger'
import Notification from '../utils/Notification'
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
      async deleteVaultKeep(id) {
        try {
          if (await Notification.confirmAction('Are you sure you want to delete this keep from this vault?', 'You won\'t be able to revert this.', '', 'Yes, Delete')) {
            await keepsService.deleteVaultKeep(id)
            Notification.toast('Successfully Deleted Keep', 'success')
          }
        } catch (error) {
          console.error(error)
        }
      },
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
.bottom{
  bottom: 0;
}
 img{
   width: 100%
 }
 .small-img{
   width: 1em;
   left: 0;
   top: 5px}
   .glow{
       box-shadow: 0 0 3em 0.5em white;
   }
   .backg{
     background-color: rgba(255, 255, 255, 0.479);
     border-radius: 5px;
   }
</style>

  //  .text{
  //      color: black;
  // -webkit-text-fill-color: black; /* Will override color (regardless of order) */
  // -webkit-text-stroke-width: .2px;
  // -webkit-text-stroke-color: white;
  //  }
