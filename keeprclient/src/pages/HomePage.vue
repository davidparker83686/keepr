<template>
  <div class="home container-fluid">
    <div class="card-columns">
      <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'Home',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    onMounted(async() => {
      try {
        // debugger
        await keepsService.getAllKeeps()
      } catch (error) {
        console.error(error)
      }
    })

    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  // > img{
  //   height: 200px;
  //   width: 200px;
  }
  // .container{
  //   width: 95%;
  //   margin: 0 auto;
  // }

.grid{
  display: flex;
}
.grid-col{
  flex: 1;
  padding: 0 .1 em;
}

.grid-col--3{
display: none;
}
.grid-col--4{
display: none;
}

@media (min-width: 768px) {
  // .container{
  //   width: 90%;
  // }
  .grid-col--3{
display: block;
}
.grid-col--4{
display: block;
}
}

</style>
