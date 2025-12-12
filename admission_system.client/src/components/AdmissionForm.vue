<script>

</script>
<template>
  <p>Age: {{ age }}</p>
  <input v-model="age" placeholder="Age" />

  <div>Visitor Type: {{ visitor_type }}</div>

  <input type="radio" id="guest" value="guest" v-model="visitor_type" />
  <label for="guest">Guest</label>

  <input type="radio" id="emlpoyee" value="emlpoyee" v-model="visitor_type" />
  <label for="emlpoyee">Emlpoyee</label>

  <input type="checkbox" id="isPass" v-model="isPass" />
  <label for="isPass">Do you have a pass? {{ isPass }}</label>

  <div>Pass level: {{ pass_level }}</div>

  <select v-model="pass_level">
    <option disabled value="">Please select Pass level</option>
    <option>1</option>
    <option>2</option>
    <option>3</option>
  </select>

  <input type="checkbox" id="isWeapon" v-model="isWeapon" />
  <label for="isWeapon">Do you have a weapon? {{ isWeapon }}</label>

  <input type="checkbox" id="isAggressive" v-model="isAggressive" />
  <label for="isAggressive">Are you aggressive? {{ isAggressive }}</label>

  <div>Zone: {{ zone }}</div>

  <select v-model="zone">
    <option disabled value="">Please select zone</option>
    <option>Meeting Room</option>
    <option>Laboratory</option>
    <option>Secret Library</option>
  </select>

  <button @click="submit">Submit</button>
</template>
