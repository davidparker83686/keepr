<template>
  <div class="home container-fluid">
    hello world
    <div class="row">
      <div class="col">
        <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'

export default {
  name: 'Home',
  setup() {
    const state = reactive({
      keep: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    onMounted(async() => {
      try {
        await keepService.getAllKeeps()
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
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
